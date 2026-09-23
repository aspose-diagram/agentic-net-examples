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

            // Configure global font settings before loading the diagram
            // Add the system font folder (recursive) so Aspose.Diagram can locate fonts
            FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);
            // Set a default fallback font that will be used when a required font is missing
            FontConfigs.DefaultFontName = "Arial";

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the list of installed system fonts using Aspose.Drawing.Text
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            var installedFontNames = installedFonts.Families
                .Select(f => f.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            // Check each font used in the diagram and report missing ones
            foreach (Font font in diagram.Fonts)
            {
                if (!installedFontNames.Contains(font.Name))
                {
                    Console.WriteLine($"Missing font detected: '{font.Name}'. It will be substituted with '{FontConfigs.DefaultFontName}'.");
                    // Specific substitution mapping is not required; the default font will be applied automatically.
                }
            }

            // Save the diagram to PDF; the default font substitution will be applied during rendering
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
