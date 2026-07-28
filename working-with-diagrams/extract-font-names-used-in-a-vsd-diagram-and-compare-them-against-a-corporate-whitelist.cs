using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file
            string diagramPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Corporate whitelist of allowed font names
            var whitelist = new[] { "Arial", "Calibri", "Times New Roman" };

            // Retrieve installed system fonts using Aspose.Drawing.Text
            var installedFonts = new InstalledFontCollection();
            var installedFontNames = installedFonts.Families
                .Select(f => f.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            // Iterate over fonts used in the diagram
            foreach (Font font in diagram.Fonts)
            {
                string fontName = font.Name;

                bool inWhitelist = whitelist.Contains(fontName, StringComparer.OrdinalIgnoreCase);
                bool isInstalled = installedFontNames.Contains(fontName);

                if (!inWhitelist)
                {
                    Console.WriteLine($"Font '{fontName}' is NOT in the corporate whitelist.");
                }

                if (!isInstalled)
                {
                    Console.WriteLine($"Font '{fontName}' is NOT installed on the system.");
                }
            }

            Console.WriteLine("Font validation completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
