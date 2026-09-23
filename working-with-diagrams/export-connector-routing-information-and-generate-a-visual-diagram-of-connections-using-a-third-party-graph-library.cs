using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main()
    {
        // Path to the source Visio file
        string sourcePath = "source.vsdx";
        // Verify source file exists
        if (!File.Exists(sourcePath)) { Console.Error.WriteLine($"File not found: {sourcePath}"); return; }

        try
        {
            // Load the source diagram
            Diagram sourceDiagram = new Diagram(sourcePath);

            // Assume we work with the first page
            Page srcPage = sourceDiagram.Pages[0];

            // Collect all shape IDs that participate in connections
            HashSet<long> nodeIds = new HashSet<long>();
            foreach (Connect conn in srcPage.Connects)
            {
                nodeIds.Add(conn.FromSheet);
                nodeIds.Add(conn.ToSheet);
            }

            // Create a new diagram for visualizing the connections
            Diagram visualDiagram = new Diagram();
            visualDiagram.Pages.Add(new Page());
            Page visPage = visualDiagram.Pages[0];

            // Map original shape IDs to new shape IDs in the visual diagram
            Dictionary<long, long> idMap = new Dictionary<long, long>();

            // Simple grid layout parameters
            double startX = 2.0;
            double startY = 2.0;
            double stepX = 4.0;
            double stepY = 3.0;
            int index = 0;

            // Add a rectangle shape for each node
            foreach (long originalId in nodeIds)
            {
                double pinX = startX + (index % 5) * stepX;
                double pinY = startY + (index / 5) * stepY;
                double width = 2.0;
                double height = 1.0;

                // Add a rectangle master shape (isCalculate parameter expects int in this version)
                long newShapeId = visualDiagram.AddShape(pinX, pinY, width, height, "Rectangle", 0);
                idMap[originalId] = newShapeId;

                // Retrieve the newly added shape to set its label
                Shape shape = visPage.Shapes.GetShape(newShapeId);
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt($"ID:{originalId}"));
                index++;
            }

            // Add connectors based on the source connections
            foreach (Connect conn in srcPage.Connects)
            {
                long srcOriginal = conn.FromSheet;
                long tgtOriginal = conn.ToSheet;

                if (!idMap.ContainsKey(srcOriginal) || !idMap.ContainsKey(tgtOriginal))
                    continue; // safety check

                long srcNewId = idMap[srcOriginal];
                long tgtNewId = idMap[tgtOriginal];

                // Create a dynamic connector shape (isCalculate parameter expects int)
                long connectorId = visualDiagram.AddShape(0, 0, 0, 0, "Dynamic connector", 0);

                // Connect the two nodes using bottom-to-top points
                visPage.ConnectShapesViaConnector(srcNewId, ConnectionPointPlace.Bottom, tgtNewId, ConnectionPointPlace.Top, connectorId);

                // Set routing style to right-angle
                Shape connector = visPage.Shapes.GetShape(connectorId);
                connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
            }

            // Export the visual diagram as a PNG image
            string outputImage = "connections.png";
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            visualDiagram.Save(outputImage, imgOptions);

            // Also save as Visio file for further editing
            string outputVisio = "connections.vsdx";
            visualDiagram.Save(outputVisio, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}