using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Output PDF/A file path
            string outputPath = "output.pdf";

            // Configure font folder (system fonts) and default fallback font
            string fontsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            FontConfigs.SetFontFolder(fontsFolder, true);
            FontConfigs.DefaultFontName = "Arial";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Validate that all font sizes meet accessibility minimum (12 pt)
            ValidateFontSizes(diagram);

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                DefaultFont = "Arial",
                SaveFormat = SaveFileFormat.Pdf
            };

            // Save the diagram as PDF/A
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Diagram exported successfully to PDF/A.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    static void ValidateFontSizes(Diagram diagram)
    {
        const double minPoints = 12.0;
        const double minInches = minPoints / 72.0; // Convert points to inches

        // Check each character's font size
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                foreach (Aspose.Diagram.Char ch in shape.Chars)
                {
                    double sizeInches = ch.Size.Value;
                    if (sizeInches < minInches)
                    {
                        double sizePoints = sizeInches * 72.0;
                        throw new Exception($"Font size {sizePoints:F1} pt is below the minimum {minPoints} pt in shape ID {shape.ID}.");
                    }
                }
            }
        }

        // Optional: warn about fonts not installed on the system
        InstalledFontCollection installedFonts = new InstalledFontCollection();
        foreach (Aspose.Diagram.Font font in diagram.Fonts)
        {
            bool isInstalled = installedFonts.Families
                .Any(f => f.Name.Equals(font.Name, StringComparison.OrdinalIgnoreCase));

            if (!isInstalled)
            {
                Console.WriteLine($"Warning: Font '{font.Name}' used in the diagram is not installed on the system.");
            }
        }
    }
}
