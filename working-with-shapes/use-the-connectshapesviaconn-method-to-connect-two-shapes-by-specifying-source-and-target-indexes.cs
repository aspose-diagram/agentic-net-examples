using System;
using System.IO;
using Aspose.Diagram;

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
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page to work with.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Use the first page.
            Page page = diagram.Pages[0];

            // -----------------------------------------------------------------
            // Add two rectangle shapes that will be connected.
            // -----------------------------------------------------------------
            // Shape 1 – positioned at (2, 5) inches.
            long shapeId1 = page.AddShape(2.0, 5.0, "Rectangle", false);
            // Shape 2 – positioned at (6, 5) inches.
            long shapeId2 = page.AddShape(6.0, 5.0, "Rectangle", false);

            // -----------------------------------------------------------------
            // Add a dynamic connector shape that will link the two rectangles.
            // -----------------------------------------------------------------
            // Place the connector roughly between the two shapes.
            long connectorId = page.AddShape(4.0, 5.0, "Dynamic connector", false);

            // -----------------------------------------------------------------
            // Connect the shapes using connection indexes.
            // The ConnectShapesViaConnectorIndex method expects:
            //   sourceShapeId, sourceConnectionIndex,
            //   targetShapeId, targetConnectionIndex,
            //   connectorShapeId
            // Connection indexes are 1‑based. Here we use index 1 for both shapes.
            // -----------------------------------------------------------------
            page.ConnectShapesViaConnectorIndex(shapeId1, 1, shapeId2, 1, connectorId);

            // -----------------------------------------------------------------
            // Save the modified diagram to the specified output file.
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}