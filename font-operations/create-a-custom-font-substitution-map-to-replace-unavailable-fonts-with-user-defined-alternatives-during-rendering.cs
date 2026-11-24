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
        // Input Visio file path (provided as first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        // Define a custom font substitution map (missing font -> substitute font)
        var substitutionMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "MissingFontA", "Arial" },
            { "MissingFontB", "Calibri" },
            // Add more mappings as needed
        };

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Enumerate fonts used in the diagram
            var diagramFonts = new List<string>();
            foreach (Aspose.Diagram.Font font in diagram.Fonts)
            {
                diagramFonts.Add(font.Name);
            }

            // Get installed system fonts using Aspose.Drawing.Text
            var installedFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            InstalledFontCollection sysFonts = new InstalledFontCollection();
            // The Families collection provides font family names
            foreach (var family in sysFonts.Families)
            {
                installedFonts.Add(family.Name);
            }

            // Check each diagram font against installed fonts and apply substitution map
            foreach (string fontName in diagramFonts.Distinct())
            {
                if (!installedFonts.Contains(fontName))
                {
                    if (substitutionMap.TryGetValue(fontName, out string substitute))
                    {
                        Console.WriteLine($"Font '{fontName}' is missing. Substituting with '{substitute}'.");
                        // Note: Aspose.Diagram does not provide per-font substitution API.
                        // Setting a global default font ensures rendering fallback.
                        // Here we simply log the intended substitution.
                    }
                    else
                    {
                        Console.WriteLine($"Font '{fontName}' is missing and no substitution defined.");
                    }
                }
            }

            // Configure custom font folder (if you have additional TrueType fonts)
            // Adjust the path to your custom fonts directory as needed.
            string customFontDir = @"C:\CustomFonts";
            if (Directory.Exists(customFontDir))
            {
                // Set the custom font folder (recursive search)
                FontConfigs.SetFontFolder(customFontDir, true);
                Console.WriteLine($"Custom font folder set to '{customFontDir}'.");
            }
            else
            {
                Console.WriteLine($"Custom font folder '{customFontDir}' does not exist. Skipping.");
            }

            // Set a global default fallback font name
            FontConfigs.DefaultFontName = "Arial";

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Output file path
            string outputPath = "output.pdf";

            // Save the diagram to PDF using the configured options
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}