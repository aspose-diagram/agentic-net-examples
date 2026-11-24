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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
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
                    // Process only shapes that contain text.
                    if (shape.Text == null || shape.Text.Value == null)
                        continue;

                    // Retrieve the plain text of the shape.
                    string fullText = shape.Text.Value.Text;
                    if (string.IsNullOrEmpty(fullText))
                        continue;

                    // Ensure the Chars collection matches the text length.
                    // If it is shorter, skip the excess characters.
                    int charCount = Math.Min(fullText.Length, shape.Chars.Count);

                    // Iterate over each character.
                    for (int i = 0; i < charCount; i++)
                    {
                        char c = fullText[i];

                        // Check if the character is a digit.
                        if (char.IsDigit(c))
                        {
                            // Set a larger font size for numeric characters.
                            // Font size is specified in inches (points / 72).
                            // Example: 24 points => 24/72 = 0.333... inches.
                            shape.Chars[i].Size.Value = 24.0 / 72.0;
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
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}