using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file (VSD, VSDX, etc.)
                string diagramPath = "example.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Corporate whitelist of allowed font names (case‑insensitive)
                var whitelist = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "Arial",
                    "Calibri",
                    "Times New Roman",
                    "Segoe UI"
                    // Add other approved fonts here
                };

                // Collect all font names used in the diagram
                var usedFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (Font font in diagram.Fonts)
                {
                    if (!string.IsNullOrEmpty(font.Name))
                    {
                        usedFonts.Add(font.Name);
                    }
                }

                // Determine fonts that are not in the whitelist
                var nonCompliantFonts = usedFonts.Except(whitelist).ToList();

                // Report the results
                if (nonCompliantFonts.Count > 0)
                {
                    Console.WriteLine("The following fonts are used in the diagram but are NOT allowed by the corporate whitelist:");
                    foreach (string fontName in nonCompliantFonts)
                    {
                        Console.WriteLine($"- {fontName}");
                    }

                    // Optionally, halt execution to enforce compliance
                    throw new Exception("Font compliance check failed.");
                }
                else
                {
                    Console.WriteLine("All fonts used in the diagram are compliant with the corporate whitelist.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }