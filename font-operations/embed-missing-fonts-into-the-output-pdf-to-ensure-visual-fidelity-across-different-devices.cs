using System.IO;
using System;
using System.Collections.Generic;
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

            // Configure font folder(s) for Aspose.Diagram
            // The second argument indicates whether to search subfolders recursively
            FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);
            // Set a fallback default font in case a required font is missing
            FontConfigs.DefaultFontName = "Arial";

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Validate fonts used in the diagram against installed system fonts
            var installedFonts = new InstalledFontCollection();
            var installedNames = installedFonts.Families
                                                .Select(f => f.Name)
                                                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (Font font in diagram.Fonts)
            {
                if (!installedNames.Contains(font.Name))
                {
                    Console.WriteLine($"Missing font detected: {font.Name}");
                }
            }

            // Prepare PDF save options with a default font fallback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF; fonts will be embedded where possible
            diagram.Save("output.pdf", pdfOptions);

            Console.WriteLine("PDF saved successfully with font embedding handling.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
