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
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: DiagramToTiff <inputVisioFile> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Guard against missing output directory.
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Error: Output directory \"{outputDir}\" does not exist.");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // Configure font handling before loading the diagram.
            // -----------------------------------------------------------------
            // Set the system font folder so Aspose.Diagram can locate installed fonts.
            string systemFontFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                "Fonts");

            if (Directory.Exists(systemFontFolder))
            {
                // Register the system font folder (recursive search enabled).
                FontConfigs.SetFontFolder(systemFontFolder, true);
            }

            // Optionally set a default fallback font.
            FontConfigs.DefaultFontName = "Arial";

            // -----------------------------------------------------------------
            // Load the Visio diagram.
            // -----------------------------------------------------------------
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Validate that all fonts used in the diagram are installed.
            // -----------------------------------------------------------------
            InstalledFontCollection installedFonts = new InstalledFontCollection();

            // Build a hash set of installed font family names for quick lookup.
            var installedFontNames = installedFonts.Families
                .Select(f => f.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (Font font in diagram.Fonts)
            {
                // Font.Name gives the font family name used in the diagram.
                if (!installedFontNames.Contains(font.Name))
                {
                    Console.Error.WriteLine($"Warning: Font \"{font.Name}\" used in the diagram is not installed on this system.");
                }
            }

            // -----------------------------------------------------------------
            // Export each page to a high‑resolution TIFF image.
            // -----------------------------------------------------------------
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure image save options for TIFF.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff)
                {
                    // Export a single page at a time.
                    PageIndex = i,
                    PageCount = 1,
                    // Set a high resolution (e.g., 300 DPI).
                    Resolution = 300f,
                    // Preserve original font rendering; set default font as fallback.
                    DefaultFont = "Arial"
                };

                // Build output file name.
                string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.tiff");

                // Save the page as TIFF.
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Saved page {i + 1} to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}