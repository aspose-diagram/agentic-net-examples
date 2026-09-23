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

            // Path to the source Visio diagram
            string inputPath = "input.vsdx";
            // Path for the resulting PDF
            string outputPath = "output.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure font folders (adjust the path as needed for your environment)
            // This ensures Aspose.Diagram can locate system fonts.
            FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);

            // Set a fallback font in case a used font is missing
            FontConfigs.DefaultFontName = "Arial";

            // Enumerate fonts used in the diagram and report any that are not installed
            var installedFonts = new InstalledFontCollection()
                .Families
                .Select(f => f.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (Font font in diagram.Fonts)
            {
                Console.WriteLine($"Diagram uses font: {font.Name}");
                if (!installedFonts.Contains(font.Name))
                {
                    Console.WriteLine($"Warning: Font \"{font.Name}\" is not installed on the system.");
                }
            }

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Fallback font for missing glyphs
            pdfOptions.DefaultFont = "Arial";
            // Explicitly set the format (required for the overload used)
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram as PDF.
            // Only the fonts actually referenced by the diagram will be embedded.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
