using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through pages (assuming first page for simplicity).
            foreach (Page page in diagram.Pages)
            {
                // Find the first connector (dynamic connector) shape.
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector by its master name "Dynamic connector".
                    // Also ensure the shape is a connector type.
                    if (shape.NameU != null && shape.NameU.Equals("Dynamic connector", StringComparison.OrdinalIgnoreCase))
                    {
                        // Set the target (end) arrowhead style to a filled triangle.
                        // Arrow style values are integer based; 4 corresponds to a filled triangle.
                        shape.Line.EndArrow.Value = 4;

                        // Optionally, ensure the arrow is visible (some styles require size > 0).
                        // Using default size; no change needed.

                        // Break after modifying the first connector.
                        break;
                    }
                }

                // Only process the first page.
                break;
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}