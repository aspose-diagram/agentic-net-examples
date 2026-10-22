using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [targetFormat]");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve system-installed fonts using Aspose.Drawing.Text
            InstalledFontCollection systemFonts = new InstalledFontCollection();

            // Build a set of installed font family names for quick lookup (case‑insensitive)
            HashSet<string> installedFontNames = new HashSet<string>(
                systemFonts.Families.Select(f => f.Name),
                StringComparer.OrdinalIgnoreCase);

            // Collect fonts used in the diagram that are NOT installed on the system
            List<string> missingFonts = new List<string>();

            foreach (Font font in diagram.Fonts)
            {
                // Font.Name provides the font family name used in the diagram
                string fontName = font.Name;

                if (!installedFontNames.Contains(fontName))
                {
                    missingFonts.Add(fontName);
                }
            }

            // Report results
            if (missingFonts.Count == 0)
            {
                Console.WriteLine("All fonts used in the diagram are installed on the system.");
            }
            else
            {
                Console.WriteLine("The following fonts used in text shapes are NOT installed on the system:");
                foreach (string missing in missingFonts.Distinct())
                {
                    Console.WriteLine($"- {missing}");
                }
            }

            // OPTIONAL: If a target output format is provided, you could extend the logic
            // to check format‑specific font support. This example focuses on system font availability.
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}