using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (assumes at least one page exists)
                Page page = diagram.Pages[0];

                // Add two rectangle shapes to the page
                // Parameters: pinX, pinY, width, height, master name, isCalculate (false)
                long rect1Id = page.AddShape(2.0, 2.0, 1.0, 1.0, "Rectangle", false);
                long rect2Id = page.AddShape(5.0, 2.0, 1.0, 1.0, "Rectangle", false);

                // Add a dynamic connector shape (zero size is acceptable for connectors)
                long connectorId = page.AddShape(0.0, 0.0, 0.0, 0.0, "Dynamic connector", false);

                // Connect the two rectangles via the connector
                // Use ConnectionPointPlace enum values to specify connection points
                page.ConnectShapesViaConnector(
                    rect1Id,
                    ConnectionPointPlace.Right,
                    rect2Id,
                    ConnectionPointPlace.Left,
                    connectorId);

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}