using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least input and output file paths
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> [fallbackFontName]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        // Optional fallback font name; default to a common Unicode font
        string fallbackFontName = args.Length >= 3 ? args[2] : "Arial Unicode MS";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Verify that the fallback font is installed on the system (optional)
        try
        {
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            bool fontFound = installedFonts.Families.Any(f =>
                string.Equals(f.Name, fallbackFontName, StringComparison.OrdinalIgnoreCase));

            if (!fontFound)
            {
                Console.Error.WriteLine($"Warning: Fallback font \"{fallbackFontName}\" is not installed on this machine.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while checking installed fonts: {ex.Message}");
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Set the global default font for missing glyphs
                FontConfigs.DefaultFontName = fallbackFontName;

                // Ensure the fallback font exists in the diagram's font collection
                int fallbackFontIndex = -1;
                for (int i = 0; i < diagram.Fonts.Count; i++)
                {
                    if (string.Equals(diagram.Fonts[i].Name, fallbackFontName, StringComparison.OrdinalIgnoreCase))
                    {
                        fallbackFontIndex = i;
                        break;
                    }
                }

                if (fallbackFontIndex == -1)
                {
                    // Add the fallback font to the diagram's font collection
                    Font fallbackFont = new Font();
                    fallbackFont.Name = fallbackFontName;
                    diagram.Fonts.Add(fallbackFont);
                    fallbackFontIndex = diagram.Fonts.Count - 1;
                }

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only shapes that contain text
                        if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.Text))
                        {
                            // Assign the fallback font to each character run
                            for (int i = 0; i < shape.Chars.Count; i++)
                            {
                                shape.Chars[i].Font.Value = fallbackFontIndex;
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}