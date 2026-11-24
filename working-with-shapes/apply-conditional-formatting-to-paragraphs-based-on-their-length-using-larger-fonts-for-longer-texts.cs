using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                for (int p = 0; p < diagram.Pages.Count; p++)
                {
                    Page page = diagram.Pages[p];

                    // Iterate through all shapes on the current page.
                    for (int s = 0; s < page.Shapes.Count; s++)
                    {
                        Shape shape = page.Shapes[s];

                        // Retrieve the shape's text (if any).
                        string text = string.Empty;
                        if (shape.Text != null && shape.Text.Value != null)
                        {
                            text = shape.Text.Value.ToString();
                        }

                        // Skip shapes without text.
                        if (string.IsNullOrEmpty(text))
                            continue;

                        // Determine font size based on text length.
                        // Example policy: short ≤20 chars → 10pt, 21‑50 chars → 12pt, >50 chars → 14pt.
                        double fontSize = 10.0;
                        int length = text.Length;
                        if (length > 50)
                            fontSize = 14.0;
                        else if (length > 20)
                            fontSize = 12.0;

                        // Apply the calculated font size to all characters in the shape.
                        for (int c = 0; c < shape.Chars.Count; c++)
                        {
                            // The Size cell holds the font size; assign the new value.
                            shape.Chars[c].Size.Value = fontSize;
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}