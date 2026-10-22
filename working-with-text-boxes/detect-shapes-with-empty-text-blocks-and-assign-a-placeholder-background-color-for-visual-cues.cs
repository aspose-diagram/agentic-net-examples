using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Determine output path (append "_modified" before the extension).
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_modified" + Path.GetExtension(inputPath));

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve plain text of the shape.
                    string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                    // Detect empty or whitespace‑only text blocks.
                    if (string.IsNullOrWhiteSpace(shapeText))
                    {
                        // Assign a placeholder background color to the text block.
                        // Using RGB() string format as required by Aspose.Diagram.
                        shape.TextBlock.TextBkgnd.Ufe.F = "RGB(255,255,0)"; // Yellow highlight

                        // Optionally make the background fully opaque.
                        shape.TextBlock.TextBkgndTrans.Value = 0;
                    }
                }
            }

            // Save the modified diagram.
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