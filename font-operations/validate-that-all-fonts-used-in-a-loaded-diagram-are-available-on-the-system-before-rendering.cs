using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the collection of installed system fonts using Aspose.Drawing.Text.
                InstalledFontCollection installedFontCollection = new InstalledFontCollection();

                // Build a hash set of installed font family names for fast lookup (case‑insensitive).
                HashSet<string> installedFontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var family in installedFontCollection.Families)
                {
                    // The family object exposes a Name property.
                    installedFontNames.Add(family.Name);
                }

                // Flag to track whether all diagram fonts are available.
                bool allFontsAvailable = true;

                // Iterate over fonts used in the diagram.
                foreach (Font font in diagram.Fonts)
                {
                    // Font.Name holds the font name as stored in the Visio file.
                    string fontName = font.Name;

                    // Check if the font is installed on the system.
                    if (!installedFontNames.Contains(fontName))
                    {
                        Console.WriteLine($"Missing font: {fontName}");
                        allFontsAvailable = false;
                    }
                }

                if (allFontsAvailable)
                {
                    Console.WriteLine("All fonts used in the diagram are available on this system.");
                }
                else
                {
                    // Configure a fallback font to avoid rendering issues.
                    FontConfigs.DefaultFontName = "Arial";
                    Console.WriteLine("Fallback font set to Arial for missing fonts.");
                }

                // At this point you could proceed with rendering/exporting the diagram,
                // knowing that font availability has been validated.
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}