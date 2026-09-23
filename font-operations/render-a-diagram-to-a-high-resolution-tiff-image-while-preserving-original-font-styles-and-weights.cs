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

            // Input Visio file path (change as needed)
            string inputPath = "input.vsdx";
            // Output TIFF file path
            string outputPath = "output.tiff";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure font folder(s) – required before rendering
            // Example uses the Windows fonts folder; adjust the path as appropriate for your environment
            FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);
            // Set a fallback default font
            FontConfigs.DefaultFontName = "Arial";

            // Validate that all fonts used in the diagram are installed on the system
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            var installedFamilies = installedFonts.Families; // dynamic collection, type not strongly typed

            foreach (Font font in diagram.Fonts)
            {
                bool isInstalled = installedFamilies
                    .Cast<object>()
                    .Any(f => f.GetType().GetProperty("Name")?.GetValue(f)?.ToString()
                              .Equals(font.Name, StringComparison.OrdinalIgnoreCase) == true);

                if (!isInstalled)
                {
                    Console.WriteLine($"Warning: Font \"{font.Name}\" used in the diagram is not installed. It will be substituted with the default font.");
                }
            }

            // Prepare high‑resolution TIFF export options
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
            saveOptions.Resolution = 300f;               // 300 DPI for high quality
            saveOptions.DefaultFont = "Arial";           // Ensure fallback font is set
            // Export all pages; adjust PageIndex/PageCount if only a subset is needed
            saveOptions.PageIndex = 0;
            saveOptions.PageCount = diagram.Pages.Count;

            // Save the diagram as a TIFF image
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram successfully rendered to high‑resolution TIFF at \"{outputPath}\".");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
