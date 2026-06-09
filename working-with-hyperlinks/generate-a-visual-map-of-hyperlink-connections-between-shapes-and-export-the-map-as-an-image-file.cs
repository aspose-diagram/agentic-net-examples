using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string sourcePath = "input.vsdx";
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the source diagram
            Diagram sourceDiagram = new Diagram(sourcePath);

            // Create a new blank diagram that will hold the visual map
            Diagram mapDiagram = new Diagram();

            // Get the first page of both diagrams (assumes single‑page documents)
            Page sourcePage = sourceDiagram.Pages[0];
            Page mapPage = mapDiagram.Pages[0];

            // Mapping from source shape ID to map shape ID
            Dictionary<long, long> shapeIdMap = new Dictionary<long, long>();

            // Layout parameters for the map shapes
            double startX = 1.0;
            double startY = 1.0;
            double boxWidth = 2.5;
            double boxHeight = 1.0;
            double verticalSpacing = 1.5;

            int index = 0;

            // First pass: create a rectangle for each source shape and store the mapping
            foreach (Shape srcShape in sourcePage.Shapes)
            {
                // Calculate position for the rectangle
                double posY = startY + index * (boxHeight + verticalSpacing);

                // Draw a rectangle on the map page
                long mapShapeId = mapPage.DrawRectangle(startX, posY, boxWidth, boxHeight);

                // Retrieve the rectangle shape to set its text
                Shape mapShape = mapPage.Shapes.GetShape(mapShapeId);
                mapShape.Text.Value.Clear();
                mapShape.Text.Value.Add(new Txt(srcShape.NameU));

                // Store the correspondence between source shape ID and map shape ID
                shapeIdMap[srcShape.ID] = mapShapeId;

                index++;
            }

            // Second pass: create connectors based on hyperlinks
            foreach (Shape srcShape in sourcePage.Shapes)
            {
                // Skip shapes without hyperlinks
                if (srcShape.Hyperlinks == null) continue;

                foreach (Hyperlink link in srcShape.Hyperlinks)
                {
                    // Expect the SubAddress to contain the target shape's NameU
                    string targetName = link.SubAddress.Value;
                    if (string.IsNullOrWhiteSpace(targetName)) continue;

                    // Find the target shape in the source diagram
                    Shape targetShape = null;
                    foreach (Shape s in sourcePage.Shapes)
                    {
                        if (s.NameU == targetName)
                        {
                            targetShape = s;
                            break;
                        }
                    }

                    if (targetShape == null) continue; // target not found

                    // Retrieve corresponding map shape IDs
                    long fromMapId = shapeIdMap[srcShape.ID];
                    long toMapId = shapeIdMap[targetShape.ID];

                    // Add a connector shape (Dynamic connector) to the map page
                    long connectorId = mapPage.AddShape(0, 0, "Dynamic connector", false);

                    // Connect the two rectangles using the connector
                    mapPage.ConnectShapesViaConnector(
                        fromMapId,
                        ConnectionPointPlace.Right,
                        toMapId,
                        ConnectionPointPlace.Left,
                        connectorId);
                }
            }

            // Export the visual map as a PNG image
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            mapDiagram.Save("hyperlink_map.png", imgOptions);

            Console.WriteLine("Hyperlink map generated and saved as 'hyperlink_map.png'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}