using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure fallback font for missing glyphs
            string fallbackFontName = "Arial Unicode MS";
            FontConfigs.DefaultFontName = fallbackFontName;

            // Verify that the fallback font is installed on the system
            InstalledFontCollection fontCollection = new InstalledFontCollection();
            bool fallbackExists = fontCollection.Families.Any(f => f.Name.Equals(fallbackFontName, StringComparison.OrdinalIgnoreCase));
            if (!fallbackExists)
            {
                throw new Exception($"Fallback font \"{fallbackFontName}\" is not installed on this machine.");
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape contains text
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.ToString()))
                    {
                        // Iterate over each text run (Txt) within the shape
                        foreach (var item in shape.Text.Value)
                        {
                            if (item is Txt txt)
                            {
                                string original = txt.Text;
                                char[] characters = original.ToCharArray();
                                bool modified = false;

                                // Replace non‑ASCII characters with a placeholder glyph
                                for (int i = 0; i < characters.Length; i++)
                                {
                                    char ch = characters[i];
                                    if (ch > 127) // Simple heuristic for missing glyphs
                                    {
                                        characters[i] = '?';
                                        modified = true;
                                    }
                                }

                                // Update the text run if any replacement occurred
                                if (modified)
                                {
                                    txt.Text = new string(characters);
                                }
                            }
                        }
                    }
                }
            }

            // Save the updated diagram using the fallback font configuration
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
