using System.IO;
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

            // Path to the Visio file – replace with your actual file path or pass as an argument
            string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Get the list of installed font names using Aspose.Drawing.Text
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            var installedFontNames = new HashSet<string>(
                installedFonts.Families.Select(f => f.Name),
                StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains any text
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                    {
                        // Collect fonts used by this shape
                        var shapeFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                        if (shape.Chars != null)
                        {
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                string fontName = ch.FontName.Value;
                                if (!string.IsNullOrEmpty(fontName))
                                {
                                    shapeFonts.Add(fontName);
                                }
                            }
                        }

                        // Identify fonts that are not installed on the system
                        var missingFonts = shapeFonts.Where(f => !installedFontNames.Contains(f)).ToList();

                        if (missingFonts.Count > 0)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' uses missing fonts:");
                            foreach (string font in missingFonts)
                            {
                                Console.WriteLine($"  - {font}");
                            }
                        }
                    }
                }
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
