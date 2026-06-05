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

            // Path to the Visio file to be loaded
            string inputPath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the collection of installed system fonts
                var installedFontCollection = new InstalledFontCollection();

                // Build a hash set of installed font names for fast lookup (case‑insensitive)
                var installedFontNames = installedFontCollection.Families
                    .Select(f => f.Name)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                // List to hold any fonts used in the diagram that are not installed
                var missingFonts = new List<string>();

                // Iterate over fonts used in the diagram (explicit type required)
                foreach (Font font in diagram.Fonts)
                {
                    string fontName = font.Name;
                    if (!installedFontNames.Contains(fontName))
                    {
                        missingFonts.Add(fontName);
                    }
                }

                // If any missing fonts are found, report and abort rendering
                if (missingFonts.Count > 0)
                {
                    Console.WriteLine("Missing fonts detected:");
                    foreach (string f in missingFonts)
                    {
                        Console.WriteLine($"- {f}");
                    }

                    // Optionally set a fallback font for rendering
                    FontConfigs.DefaultFontName = "Arial";

                    // Abort further processing
                    throw new Exception("Required fonts are missing. Rendering aborted.");
                }
                else
                {
                    Console.WriteLine("All fonts used in the diagram are available on the system.");
                }

                // Example rendering: save the diagram as PDF
                var pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial"; // fallback font for any unexpected substitutions
                diagram.Save("output.pdf", pdfOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
