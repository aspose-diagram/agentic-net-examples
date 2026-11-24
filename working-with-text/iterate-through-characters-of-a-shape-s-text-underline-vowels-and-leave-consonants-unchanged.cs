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
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the plain text of the shape.
                    string plainText = shape.Text.Value.Text;

                    // Process only shapes that contain text.
                    if (string.IsNullOrEmpty(plainText))
                        continue;

                    // Iterate over each character in the text.
                    for (int i = 0; i < plainText.Length; i++)
                    {
                        char c = plainText[i];

                        // Check if the character is a vowel (case‑insensitive).
                        if ("AEIOUaeiou".IndexOf(c) >= 0)
                        {
                            // Ensure the character index exists in the Chars collection.
                            if (i < shape.Chars.Count)
                            {
                                // Apply underline style to the vowel character.
                                shape.Chars[i].Style.Value = StyleValue.Underline;
                            }
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}