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

                // Expect the Visio file path as the first argument
                if (args.Length == 0)
                {
                    Console.WriteLine("Please provide the path to the Visio file as a command‑line argument.");
                    return;
                }

                string visioPath = args[0];

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Get the list of installed system fonts (case‑insensitive)
                InstalledFontCollection installedFontCollection = new InstalledFontCollection();
                HashSet<string> installedFontNames = new HashSet<string>(
                    installedFontCollection.Families.Select(f => f.Name),
                    StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape contains any text
                        if (shape.Text == null || string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                            continue;

                        // Collect distinct font names used in the shape's character runs
                        HashSet<string> shapeFontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            if (ch.FontName != null && !string.IsNullOrWhiteSpace(ch.FontName.Value))
                            {
                                shapeFontNames.Add(ch.FontName.Value);
                            }
                        }

                        // If no explicit character formatting, the shape may rely on the default font;
                        // include the diagram's default font for validation.
                        if (shapeFontNames.Count == 0 && !string.IsNullOrWhiteSpace(diagram.Fonts[0]?.Name))
                        {
                            shapeFontNames.Add(diagram.Fonts[0].Name);
                        }

                        // Determine which fonts are missing from the installed collection
                        List<string> missingFonts = shapeFontNames
                            .Where(fn => !installedFontNames.Contains(fn))
                            .ToList();

                        if (missingFonts.Count > 0)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} (NameU: {shape.NameU}) uses unsupported fonts:");
                            foreach (string missing in missingFonts)
                            {
                                Console.WriteLine($"  - {missing}");
                            }
                        }
                    }
                }

                Console.WriteLine("Font validation completed.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }