using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.pdf";
        string fontFolder = @"C:\Fonts";          // Folder containing the custom font files
        string customFontName = "CustomFont";     // Exact name of the font to apply

        // Configure Aspose.Diagram to search for fonts in the specified folder
        FontConfigs.SetFontFolder(fontFolder, true);
        FontConfigs.DefaultFontName = customFontName;

        // Optional: warn if the custom font is not installed on the system, but continue processing
        InstalledFontCollection installedFonts = new InstalledFontCollection();
        bool fontExists = false;
        foreach (var family in installedFonts.Families)
        {
            if (family.Name.Equals(customFontName, StringComparison.OrdinalIgnoreCase))
            {
                fontExists = true;
                break;
            }
        }
        if (!fontExists)
        {
            Console.Error.WriteLine($"Warning: The font \"{customFontName}\" was not found in the installed fonts. The font folder will be used for rendering.");
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Apply the custom font to every shape that contains text
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape has any visible text
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                    {
                        // Update each character run to use the custom font
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            ch.FontName.Value = customFontName;
                        }
                    }
                }
            }

            // Prepare PDF save options (fallback font is set above)
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                DefaultFont = customFontName
            };

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Diagram has been saved to PDF with the custom font applied.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}