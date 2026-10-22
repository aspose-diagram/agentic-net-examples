using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Dictionary mapping shape IDs to a list of custom connection points.
            Dictionary<long, List<Connection>> shapeConnectionMap = new Dictionary<long, List<Connection>>();

            // -----------------------------------------------------------------
            // STEP 1: Populate the dictionary with sample connection points.
            // For demonstration we add two connection points to every shape on
            // the active page. In a real scenario you would add points only to
            // the shapes that require them.
            // -----------------------------------------------------------------
            Page activePage = diagram.ActivePage;
            foreach (Shape shape in activePage.Shapes)
            {
                // Create a list to hold new connections for this shape.
                List<Connection> newConnections = new List<Connection>();

                // Example connection point at the middle of the top edge.
                Connection topMid = new Connection();
                topMid.X.Ufe.F = "Width*0.5";
                topMid.Y.Ufe.F = "0";
                newConnections.Add(topMid);

                // Example connection point at the middle of the bottom edge.
                Connection bottomMid = new Connection();
                bottomMid.X.Ufe.F = "Width*0.5";
                bottomMid.Y.Ufe.F = "Height";
                newConnections.Add(bottomMid);

                // Store the list in the dictionary using the shape's ID.
                shapeConnectionMap[shape.ID] = newConnections;
            }

            // -----------------------------------------------------------------
            // STEP 2: Batch update – add the stored connection points to each shape.
            // -----------------------------------------------------------------
            foreach (KeyValuePair<long, List<Connection>> kvp in shapeConnectionMap)
            {
                long shapeId = kvp.Key;
                List<Connection> connectionsToAdd = kvp.Value;

                // Retrieve the shape by its ID.
                Shape targetShape = activePage.Shapes.GetShape(shapeId);
                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found.");
                    continue;
                }

                // Add each connection point to the shape's Connections collection.
                foreach (Connection conn in connectionsToAdd)
                {
                    targetShape.Connections.Add(conn);
                }
            }

            // -----------------------------------------------------------------
            // STEP 3: Save the modified diagram.
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}