using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a Text object with a value.
                        if (shape.Text == null || shape.Text.Value == null)
                            continue;

                        // Retrieve the plain text of the shape.
                        string plainText = shape.Text.Value.Text;
                        if (string.IsNullOrEmpty(plainText))
                            continue;

                        // Ensure the shape has character formatting cells.
                        if (shape.Chars == null || shape.Chars.Count == 0)
                            continue;

                        // Determine the number of characters we can process.
                        int processCount = Math.Min(plainText.Length, shape.Chars.Count);

                        // Iterate over each character position.
                        for (int i = 0; i < processCount; i++)
                        {
                            char c = plainText[i];
                            bool isVowel = "AEIOUaeiou".IndexOf(c) >= 0;

                            if (isVowel)
                            {
                                // Apply underline style to the vowel character.
                                Aspose.Diagram.Char charFormat = shape.Chars[i];
                                // Preserve existing style bits and add underline.
                                charFormat.Style.Value = charFormat.Style.Value | StyleValue.Underline;
                            }
                            // Consonants are left unchanged.
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}