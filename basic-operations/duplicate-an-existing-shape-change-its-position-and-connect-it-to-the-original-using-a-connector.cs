using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape to duplicate
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The page contains no shapes to duplicate.");
                return;
            }

            // Get the first shape as the source shape
            Shape sourceShape = page.Shapes.GetShape(page.Shapes[0].ID);

            // Verify the source shape has a master (required for duplication)
            if (sourceShape.Master == null)
            {
                Console.Error.WriteLine("The source shape does not have an associated master.");
                return;
            }

            // Duplicate the shape by adding a new shape with the same master
            // Use the source shape's current position as the initial location
            double originalPinX = sourceShape.XForm.PinX.Value;
            double originalPinY = sourceShape.XForm.PinY.Value;
            string masterName = sourceShape.Master.Name;

            // Add the duplicated shape (isCalculate = false)
            long duplicateShapeId = page.AddShape(originalPinX, originalPinY, masterName, false);
            Shape duplicateShape = page.Shapes.GetShape(duplicateShapeId);

            // Change the position of the duplicated shape (e.g., offset by 2 inches)
            double offsetX = 2.0; // inches
            double offsetY = 2.0; // inches
            duplicateShape.XForm.PinX.Value = originalPinX + offsetX;
            duplicateShape.XForm.PinY.Value = originalPinY + offsetY;

            // Add a dynamic connector shape (isCalculate = false)
            long connectorId = page.AddShape(0, 0, "Dynamic connector", false);
            Shape connectorShape = page.Shapes.GetShape(connectorId);

            // Optionally configure the connector (e.g., set arrowheads)
            connectorShape.Line.BeginArrow.Value = 4; // Arrow style
            connectorShape.Line.EndArrow.Value = 4;   // Arrow style

            // Connect the original shape to the duplicated shape using the connector
            // Connect from the bottom of the source shape to the top of the duplicate shape
            page.ConnectShapesViaConnector(
                sourceShape.ID,
                ConnectionPointPlace.Bottom,
                duplicateShape.ID,
                ConnectionPointPlace.Top,
                connectorId);

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram processed successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}