using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has text
                        if (shape.Text == null || string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                            continue;

                        // Get the plain text of the shape
                        string originalText = shape.Text.Value.Text;

                        // Replace Unicode characters (code point > 127) with a fallback glyph (e.g., '?')
                        char[] chars = originalText.ToCharArray();
                        for (int i = 0; i < chars.Length; i++)
                        {
                            if (chars[i] > 127)
                            {
                                // Simple replacement; in a real scenario you could map to specific glyphs
                                chars[i] = '?';
                            }
                        }
                        string replacedText = new string(chars);

                        // Update the shape's text only if it changed
                        if (replacedText != originalText)
                        {
                            // Clear existing text runs
                            shape.Text.Value.Clear();

                            // Add the new text run
                            shape.Text.Value.Add(new Txt(replacedText));
                        }
                    }
                }

                // Configure save options with a fallback font
                DiagramSaveOptions saveOptions = new DiagramSaveOptions
                {
                    // Use a font that contains a wide range of Unicode glyphs
                    DefaultFont = "Arial Unicode MS"
                };

                // Save the modified diagram
                diagram.Save(outputPath, saveOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }