using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram (VSD) file
            string diagramPath = "input.vsd";

            // Load the diagram using the provided constructor (lifecycle rule)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Collect all font names used in the diagram
                HashSet<string> usedFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (Font font in diagram.Fonts)
                {
                    // The Font class exposes the Name property (font name)
                    usedFonts.Add(font.Name);
                }

                // Corporate whitelist of allowed fonts
                HashSet<string> whitelist = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "Arial",
                    "Calibri",
                    "Times New Roman",
                    // Add additional approved fonts here
                };

                // Determine fonts that are not in the whitelist
                List<string> nonWhitelisted = usedFonts.Except(whitelist).ToList();

                // Output results
                Console.WriteLine("Fonts used in the diagram:");
                foreach (string f in usedFonts)
                    Console.WriteLine($"- {f}");

                Console.WriteLine("\nFonts not in the corporate whitelist:");
                if (nonWhitelisted.Count == 0)
                    Console.WriteLine("All fonts are compliant.");
                else
                    foreach (string f in nonWhitelisted)
                        Console.WriteLine($"- {f}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
