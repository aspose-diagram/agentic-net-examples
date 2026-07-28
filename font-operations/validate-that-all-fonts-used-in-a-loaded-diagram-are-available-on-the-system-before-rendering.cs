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

            // Path to the input Visio file
            string inputPath = "input.vsdx";
            // Path to the output PDF file (rendering target)
            string outputPath = "output.pdf";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect system-installed font names
                InstalledFontCollection installedFonts = new InstalledFontCollection();
                var systemFontNames = installedFonts.Families
                    .Select(f => f.Name)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                // List to hold any missing fonts
                List<string> missingFonts = new List<string>();

                // Iterate over fonts used in the diagram (explicit typing required)
                foreach (Font font in diagram.Fonts)
                {
                    if (!systemFontNames.Contains(font.Name))
                    {
                        missingFonts.Add(font.Name);
                    }
                }

                // If there are missing fonts, report and abort rendering
                if (missingFonts.Count > 0)
                {
                    Console.WriteLine("The following fonts are used in the diagram but are not installed on the system:");
                    foreach (string name in missingFonts)
                    {
                        Console.WriteLine($"- {name}");
                    }
                    throw new Exception("Missing required fonts. Rendering aborted.");
                }

                // All fonts are available; proceed to render (save as PDF)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Set a fallback font just in case
                pdfOptions.DefaultFont = "Arial";

                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram rendered successfully to '{outputPath}'.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
