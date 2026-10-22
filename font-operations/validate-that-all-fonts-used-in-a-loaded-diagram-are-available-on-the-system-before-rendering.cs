using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputImagePath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : "output.png";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the list of font names used in the diagram.
            List<string> diagramFontNames = new List<string>();
            foreach (Font font in diagram.Fonts) // explicit type as required
            {
                diagramFontNames.Add(font.Name);
            }

            // Enumerate installed system fonts using Aspose.Drawing.Text.
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            HashSet<string> installedFontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var family in installedFonts.Families) // implicit typing per guidelines
            {
                // The family object provides a Name property.
                installedFontNames.Add(family.Name);
            }

            // Determine which diagram fonts are missing on the system.
            List<string> missingFonts = diagramFontNames
                .Where(name => !installedFontNames.Contains(name))
                .Distinct()
                .ToList();

            if (missingFonts.Count > 0)
            {
                Console.Error.WriteLine("The following fonts required by the diagram are NOT installed on this system:");
                foreach (string missing in missingFonts)
                {
                    Console.Error.WriteLine($"- {missing}");
                }

                // Choose a fallback font that is guaranteed to exist (e.g., Arial).
                const string fallbackFont = "Arial";
                if (installedFontNames.Contains(fallbackFont))
                {
                    Console.WriteLine($"Setting fallback font to \"{fallbackFont}\".");
                    FontConfigs.DefaultFontName = fallbackFont;
                }
                else
                {
                    Console.Error.WriteLine("No suitable fallback font found. Rendering aborted.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("All fonts used in the diagram are available on the system.");
            }

            // Proceed with rendering the first page to PNG.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                PageIndex = 0,
                PageCount = 1
            };

            diagram.Save(outputPath, saveOptions);
            Console.WriteLine($"Diagram rendered successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}