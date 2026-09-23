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

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the exported HTML file
            string outputPath = "output.html";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure font folder (system fonts) and default fallback font (web‑safe)
            // The second parameter indicates recursive search
            FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);
            FontConfigs.DefaultFontName = "Arial";

            // Verify that all fonts used in the diagram are installed on the system
            // Use Aspose.Drawing.Text.InstalledFontCollection for enumeration
            InstalledFontCollection installedFonts = new InstalledFontCollection();

            foreach (Font diagramFont in diagram.Fonts)
            {
                bool isInstalled = installedFonts.Families.Any(f => string.Equals(f.Name, diagramFont.Name, StringComparison.OrdinalIgnoreCase));
                if (!isInstalled)
                {
                    Console.WriteLine($"Warning: Font \"{diagramFont.Name}\" is not installed. It will be substituted with the default font \"Arial\".");
                }
            }

            // Prepare HTML export options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            // Export only visible pages (optional)
            htmlOptions.ExportHiddenPage = false;
            // Ensure the default font is used for missing fonts
            htmlOptions.DefaultFont = "Arial";

            // Save the diagram as HTML with embedded web‑safe fonts
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine("Diagram exported to HTML successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
