using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Set fallback font to handle missing Unicode glyphs
            // This font will be used when a character is not found in the original fonts.
            FontConfigs.DefaultFontName = "Arial Unicode MS";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has text
                    if (shape.Text == null || shape.Text.Value == null)
                        continue;

                    // Iterate through each text segment (FormatTxt) in the shape
                    foreach (FormatTxt fmt in shape.Text.Value)
                    {
                        // Cast to Txt to access the actual text string
                        Txt txt = fmt as Txt;
                        if (txt == null)
                            continue;

                        // Replace non-ASCII Unicode characters with a placeholder '?'
                        string original = txt.Text;
                        string replaced = ReplaceNonAscii(original);

                        // Update the text only if a change was made
                        if (!original.Equals(replaced))
                        {
                            txt.Text = replaced;
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Replaces characters with Unicode code points greater than 127 with a '?' placeholder.
    /// </summary>
    /// <param name="input">The original text.</param>
    /// <returns>The text with non-ASCII characters replaced.</returns>
    private static string ReplaceNonAscii(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        char[] chars = input.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] > 127)
            {
                chars[i] = '?';
            }
        }
        return new string(chars);
    }
}