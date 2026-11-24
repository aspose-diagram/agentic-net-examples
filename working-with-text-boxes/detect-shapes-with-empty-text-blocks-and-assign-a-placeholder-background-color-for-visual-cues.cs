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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the plain text of the shape.
                    string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                    // Detect empty or whitespace‑only text blocks.
                    if (string.IsNullOrWhiteSpace(shapeText))
                    {
                        // Assign a placeholder background color to the text block.
                        // Using RGB() string format as required by Aspose.Diagram.
                        shape.TextBlock.TextBkgnd.Ufe.F = "RGB(255,255,0)"; // Yellow highlight

                        // Optionally ensure the background is visible (solid fill).
                        shape.TextBlock.TextBkgndTrans.Value = 0; // Opaque
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram processed and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}