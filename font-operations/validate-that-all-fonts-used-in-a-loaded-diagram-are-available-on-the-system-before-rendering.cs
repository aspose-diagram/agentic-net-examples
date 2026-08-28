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

                // Expect the first argument to be the path of the Visio file to load.
                if (args.Length == 0)
                {
                    Console.WriteLine("Please provide the path to the Visio diagram as a command‑line argument.");
                    return;
                }

                string diagramPath = args[0];

                // Load the diagram.
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Retrieve the fonts used in the diagram.
                    // NOTE: Explicit type declaration is required for Aspose.Diagram.Font enumeration.
                    List<string> diagramFontNames = new List<string>();
                    foreach (Font font in diagram.Fonts)
                    {
                        // Font.Name gives the font family name used in the diagram.
                        diagramFontNames.Add(font.Name);
                    }

                    // Get the collection of fonts installed on the system via Aspose.Drawing.Text.
                    InstalledFontCollection installedFonts = new InstalledFontCollection();

                    // Build a set of installed font family names for fast lookup.
                    // The family objects may not have a strongly typed name property, so we use dynamic access.
                    HashSet<string> installedFontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    foreach (var family in installedFonts.Families)
                    {
                        // Attempt to read the family name via reflection if necessary.
                        // Most implementations expose a 'Name' property.
                        var nameProp = family.GetType().GetProperty("Name");
                        if (nameProp != null)
                        {
                            string familyName = nameProp.GetValue(family) as string;
                            if (!string.IsNullOrEmpty(familyName))
                            {
                                installedFontNames.Add(familyName);
                            }
                        }
                    }

                    // Identify any fonts used in the diagram that are missing on the system.
                    List<string> missingFonts = diagramFontNames
                        .Where(df => !installedFontNames.Contains(df))
                        .Distinct()
                        .ToList();

                    if (missingFonts.Count > 0)
                    {
                        Console.WriteLine("The following fonts are used in the diagram but are NOT installed on this system:");
                        foreach (string missing in missingFonts)
                        {
                            Console.WriteLine($"- {missing}");
                        }

                        // Optionally, you could set a fallback font before rendering.
                        // For example: FontConfigs.DefaultFontName = "Arial";
                        // FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);
                        throw new Exception("Missing fonts detected. Rendering aborted.");
                    }
                    else
                    {
                        Console.WriteLine("All fonts used in the diagram are available on the system.");
                    }

                    // Proceed with rendering or saving the diagram here, knowing that fonts are valid.
                    // Example: diagram.Save("output.pdf", new Aspose.Diagram.Saving.PdfSaveOptions());
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }