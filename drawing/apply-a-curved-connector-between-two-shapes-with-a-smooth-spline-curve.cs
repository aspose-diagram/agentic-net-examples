using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Find the first two non‑connector shapes (OneD == false)
                long firstShapeId = -1;
                long secondShapeId = -1;

                foreach (Shape shape in page.Shapes)
                {
                    if (!shape.OneD) // Non‑connector shape
                    {
                        if (firstShapeId == -1)
                            firstShapeId = shape.ID;
                        else if (secondShapeId == -1)
                        {
                            secondShapeId = shape.ID;
                            break;
                        }
                    }
                }

                // Validate that we have two shapes to connect
                if (firstShapeId == -1 || secondShapeId == -1)
                {
                    Console.Error.WriteLine("The diagram must contain at least two non‑connector shapes.");
                    return;
                }

                // Add a dynamic connector shape to the page
                // Parameters: pinX, pinY, width, height, masterName, isCalculate (bool)
                long connectorId = page.AddShape(0, 0, 0, 0, "Dynamic connector", false);

                // Retrieve the connector shape object
                Shape connector = page.Shapes.GetShape(connectorId);

                // Set the connector to use a curved routing style (smooth spline)
                connector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);

                // Connect the two shapes using the connector
                // Using bottom of first shape and top of second shape as example connection points
                page.ConnectShapesViaConnector(
                    firstShapeId,
                    ConnectionPointPlace.Bottom,
                    secondShapeId,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}