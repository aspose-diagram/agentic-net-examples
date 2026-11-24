using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, shape ID
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <shapeId>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse shape ID
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page (or you could select by name if needed)
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page.");
                    return;
                }

                // ------------------------------------------------------------
                // Disable line inheritance by assigning current inherited values
                // to the shape's own cells. This breaks the inheritance link.
                // ------------------------------------------------------------
                shape.Line.LineColor.Value = shape.Line.LineColor.Value;
                shape.Line.LineWeight.Value = shape.Line.LineWeight.Value;
                shape.Line.LinePattern.Value = shape.Line.LinePattern.Value;
                shape.Line.BeginArrow.Value = shape.Line.BeginArrow.Value;
                shape.Line.EndArrow.Value = shape.Line.EndArrow.Value;
                // Add other line properties here if needed.

                // ------------------------------------------------------------
                // Assign a new line weight (thickness) to the shape.
                // Value is in inches; adjust as required.
                // ------------------------------------------------------------
                double newLineWeight = 0.05; // Example: 0.05 inches
                shape.Line.LineWeight.Value = newLineWeight;

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}