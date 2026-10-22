using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page (adjust as needed)
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                // Specify the shape ID to modify (example uses ID = 1)
                long shapeId = 1;

                // Retrieve the shape; guard against missing shape
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page.");
                    return;
                }

                // ------------------------------------------------------------
                // Disable line inheritance by assigning current values to themselves
                // This breaks the inheritance chain for line properties.
                // ------------------------------------------------------------
                shape.Line.LineColor.Value = shape.Line.LineColor.Value;
                shape.Line.LinePattern.Value = shape.Line.LinePattern.Value;
                shape.Line.BeginArrow.Value = shape.Line.BeginArrow.Value;
                shape.Line.EndArrow.Value = shape.Line.EndArrow.Value;
                shape.Line.LineWeight.Value = shape.Line.LineWeight.Value;

                // ------------------------------------------------------------
                // Assign a new line weight (thickness) in inches.
                // ------------------------------------------------------------
                shape.Line.LineWeight.Value = 0.05; // Example: 0.05 inches

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}