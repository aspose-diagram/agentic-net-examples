using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file (VSD/VSDX etc.)
                string diagramPath = "sample.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Corporate whitelist of allowed font names
                List<string> whitelist = new List<string>
                {
                    "Arial",
                    "Calibri",
                    "Times New Roman"
                    // Add other approved fonts here
                };

                // Collect font names used in the diagram
                List<string> diagramFonts = new List<string>();
                foreach (Font font in diagram.Fonts)
                {
                    // Font.Name provides the font name as a string
                    diagramFonts.Add(font.Name);
                }

                // Identify fonts that are not in the whitelist
                List<string> nonWhitelistedFonts = diagramFonts
                    .Where(f => !whitelist.Contains(f, StringComparer.OrdinalIgnoreCase))
                    .Distinct()
                    .ToList();

                if (nonWhitelistedFonts.Count > 0)
                {
                    Console.WriteLine("The following fonts are used in the diagram but are NOT in the corporate whitelist:");
                    foreach (string f in nonWhitelistedFonts)
                    {
                        Console.WriteLine($"- {f}");
                    }

                    // Optionally, you can fail the process
                    throw new Exception("Font validation failed due to non‑whitelisted fonts.");
                }
                else
                {
                    Console.WriteLine("All fonts used in the diagram are compliant with the corporate whitelist.");
                }

                // OPTIONAL: Verify that the fonts used are installed on the system
                // (This step uses Aspose.Drawing.Text.InstalledFontCollection)
                InstalledFontCollection installedFonts = new InstalledFontCollection();

                // Build a set of installed font names (case‑insensitive)
                HashSet<string> installedFontNames = new HashSet<string>(
                    installedFonts.Families.Select(f => f.Name),
                    StringComparer.OrdinalIgnoreCase);

                // Find any diagram fonts that are missing from the system
                List<string> missingSystemFonts = diagramFonts
                    .Where(f => !installedFontNames.Contains(f))
                    .Distinct()
                    .ToList();

                if (missingSystemFonts.Count > 0)
                {
                    Console.WriteLine("The following fonts are used in the diagram but are NOT installed on this machine:");
                    foreach (string f in missingSystemFonts)
                    {
                        Console.WriteLine($"- {f}");
                    }
                    // You may choose to handle this situation as needed
                }
                else
                {
                    Console.WriteLine("All fonts used in the diagram are installed on the system.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }