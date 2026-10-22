using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths from command‑line arguments.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the plain text of the shape.
                    // shape.Text may be null for non‑text shapes; handle gracefully.
                    string plainText = string.Empty;
                    if (shape.Text != null && shape.Text.Value != null)
                    {
                        // shape.Text.Value.ToString() concatenates all Txt runs.
                        plainText = shape.Text.Value.ToString();
                    }

                    // Compute character count.
                    int charCount = plainText.Length;

                    // Annotate the shape with its character count.
                    // Use the shape's comment field (shape.Misc.Comment) for annotation.
                    if (shape.Misc != null && shape.Misc.Comment != null)
                    {
                        shape.Misc.Comment.Value = charCount.ToString();
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format.
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