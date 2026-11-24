using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output folder path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: verify output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Console.Error.WriteLine($"Output folder does not exist: {outputFolder}");
            return;
        }

        // Configure fallback font before loading the diagram
        FontConfigs.DefaultFontName = "Arial";

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate that fonts used in the diagram are installed on the system
                ValidateFonts(diagram);

                // Prepare high‑resolution image save options for TIFF
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
                // Set high DPI resolution (e.g., 300 DPI)
                saveOptions.Resolution = 300f;
                // Set fallback font in case a required font is missing
                saveOptions.DefaultFont = "Arial";

                // Export each page as a separate high‑resolution TIFF file
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    saveOptions.PageIndex = i;
                    saveOptions.PageCount = 1;

                    string pageFileName = $"{Path.GetFileNameWithoutExtension(inputPath)}_page{i + 1}.tiff";
                    string outputPath = Path.Combine(outputFolder, pageFileName);

                    diagram.Save(outputPath, saveOptions);
                    Console.WriteLine($"Saved page {i + 1} to {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to check that all fonts used in the diagram are installed on the system
    private static void ValidateFonts(Diagram diagram)
    {
        // Enumerate installed fonts using Aspose.Drawing.Text
        InstalledFontCollection installedFonts = new InstalledFontCollection();

        // Build a set of installed font names for quick lookup (case‑insensitive)
        var installedFontNames = installedFonts.Families
            .Select(f => f.Name)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        // Iterate over fonts referenced in the diagram
        foreach (Font font in diagram.Fonts)
        {
            if (!installedFontNames.Contains(font.Name))
            {
                Console.Error.WriteLine($"Warning: Font \"{font.Name}\" used in diagram is not installed on the system.");
            }
        }
    }
}