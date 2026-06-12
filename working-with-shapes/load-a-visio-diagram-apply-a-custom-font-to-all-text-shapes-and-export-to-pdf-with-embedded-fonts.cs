using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path for the exported PDF
        string outputPath = "output.pdf";

        // Name of the custom font to apply (must be installed or located in a font folder)
        string customFontName = "MyCustomFont";

        try
        {
            // -----------------------------------------------------------------
            // 1. Configure font directories and default font before loading diagram
            // -----------------------------------------------------------------
            // Add the system fonts folder (adjust the path as needed)
            FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);
            // Set the fallback font name used during rendering
            FontConfigs.DefaultFontName = customFontName;

            // -----------------------------------------------------------------
            // 2. Verify that the custom font is available on the system
            // -----------------------------------------------------------------
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            bool fontExists = installedFonts.Families.Any(f => f.Name.Equals(customFontName, StringComparison.OrdinalIgnoreCase));
            if (!fontExists)
            {
                Console.Error.WriteLine($"Warning: The font \"{customFontName}\" is not installed on this machine. The default fallback font will be used.");
            }

            // -----------------------------------------------------------------
            // 3. Load the Visio diagram
            // -----------------------------------------------------------------
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // 4. List fonts currently used in the diagram (for diagnostic purposes)
            // -----------------------------------------------------------------
            Console.WriteLine("Fonts referenced in the diagram before modification:");
            foreach (Font font in diagram.Fonts)
            {
                Console.WriteLine($"- {font.Name}");
            }

            // -----------------------------------------------------------------
            // 5. Apply the custom font to every shape that contains text
            // -----------------------------------------------------------------
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape actually has text content
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                    {
                        // If the shape has no character runs, create one covering the whole text
                        if (shape.Chars.Count == 0)
                        {
                            Aspose.Diagram.Char newChar = new Aspose.Diagram.Char();
                            newChar.IX = 0; // start index
                            newChar.FontName.Value = customFontName;
                            shape.Chars.Add(newChar);
                        }
                        else
                        {
                            // Update all existing character runs
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                ch.FontName.Value = customFontName;
                            }
                        }
                    }
                }
            }

            // -----------------------------------------------------------------
            // 6. Prepare PDF save options (fonts will be embedded automatically if found)
            // -----------------------------------------------------------------
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = customFontName; // fallback font for missing glyphs

            // -----------------------------------------------------------------
            // 7. Save the diagram as PDF
            // -----------------------------------------------------------------
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram has been saved to PDF with the font \"{customFontName}\" applied to all text shapes.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}