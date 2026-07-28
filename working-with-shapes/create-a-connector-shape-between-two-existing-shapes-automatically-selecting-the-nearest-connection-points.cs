using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Expect four arguments: input file, output file, first shape name, second shape name
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: <inputVisio> <outputVisio> <shapeName1> <shapeName2>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        string shapeName1 = args[2];
        string shapeName2 = args[3];

        try
        {
            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);
            Page page = diagram.Pages[0];

            // Locate the two shapes by their universal names (NameU)
            long shapeId1 = -1;
            long shapeId2 = -1;

            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == shapeName1)
                {
                    shapeId1 = shape.ID;
                }
                else if (shape.NameU == shapeName2)
                {
                    shapeId2 = shape.ID;
                }
            }

            // Verify both shapes were found
            if (shapeId1 == -1 || shapeId2 == -1)
            {
                Console.Error.WriteLine("Error: One or both shapes not found in the diagram.");
                return;
            }

            // Add a dynamic connector shape (position will be adjusted automatically)
            long connectorId = diagram.AddShape(0, 0, "Dynamic connector", 0);

            // Connect the two shapes using the nearest (center) connection points
            page.ConnectShapesViaConnector(
                shapeId1,
                ConnectionPointPlace.Center,
                shapeId2,
                ConnectionPointPlace.Center,
                connectorId);

            // Optional: set a right‑angle routing style for the connector
            Shape connector = page.Shapes.GetShape(connectorId);
            connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}