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

            // Iterate through all pages.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the current page.
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Retrieve the plain text of the shape.
                    string text = shape.Text.Value.Text;

                    // Skip shapes without text.
                    if (string.IsNullOrEmpty(text))
                        continue;

                    // Ensure the character collection is available.
                    if (shape.Chars == null || shape.Chars.Count == 0)
                        continue;

                    // Iterate over each character position.
                    // The character formatting collection aligns with the text string.
                    int charCount = Math.Min(text.Length, shape.Chars.Count);
                    for (int i = 0; i < charCount; i++)
                    {
                        char c = text[i];

                        // If the character is a digit, change its font size.
                        if (char.IsDigit(c))
                        {
                            // Set font size to 0.3 inches (approximately 21.6 points).
                            shape.Chars[i].Size.Value = 0.3;
                        }
                        // Non‑numeric characters are left unchanged.
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}