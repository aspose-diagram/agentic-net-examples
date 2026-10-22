using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // -----------------------------------------------------------------
            // Retrieve two non‑connector shapes to be linked.
            // -----------------------------------------------------------------
            long firstShapeId = -1;
            long secondShapeId = -1;

            foreach (Shape shape in page.Shapes)
            {
                // Skip connector shapes (OneD == true).
                if (shape.OneD == true)
                    continue;

                if (firstShapeId == -1)
                    firstShapeId = shape.ID;
                else if (secondShapeId == -1)
                {
                    secondShapeId = shape.ID;
                    break;
                }
            }

            // If not enough shapes exist, create them.
            if (firstShapeId == -1)
            {
                long id = page.AddShape(2, 2, "Rectangle", false);
                firstShapeId = id;
            }

            if (secondShapeId == -1)
            {
                long id = page.AddShape(5, 5, "Rectangle", false);
                secondShapeId = id;
            }

            // -----------------------------------------------------------------
            // Add a dynamic connector shape that will be used to connect the two shapes.
            // -----------------------------------------------------------------
            long connectorId = page.AddShape(0, 0, "Dynamic connector", false);

            // -----------------------------------------------------------------
            // Attempt to attach the connector. If gluing is disabled, the operation
            // will throw an exception which we catch and report.
            // -----------------------------------------------------------------
            try
            {
                // Connect the first shape's Bottom to the second shape's Top using the connector.
                page.ConnectShapesViaConnector(
                    firstShapeId,
                    ConnectionPointPlace.Bottom,
                    secondShapeId,
                    ConnectionPointPlace.Top,
                    connectorId);

                Console.WriteLine("Connector attached successfully.");
            }
            catch (Exception attachEx)
            {
                // Handle errors that arise due to gluing being disabled or other issues.
                Console.Error.WriteLine("Failed to attach connector. Possible cause: gluing is disabled for the target shapes.");
                Console.Error.WriteLine($"Error details: {attachEx.Message}");
                // Optionally, remove the connector shape to keep the diagram clean.
                // Note: Deleting a shape is done by setting its Delete flag.
                Shape connectorShape = page.Shapes.GetShape(connectorId);
                connectorShape.Del = BOOL.True;
            }

            // -----------------------------------------------------------------
            // Save the modified diagram.
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // General error handling for loading, saving, or unexpected failures.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}