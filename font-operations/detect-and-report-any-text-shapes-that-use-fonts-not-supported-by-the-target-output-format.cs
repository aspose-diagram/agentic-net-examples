using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file to be analyzed
        string diagramPath = "input.vsdx";

        // Verify the input file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Build a set of installed system font names using Aspose.Drawing.Text
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            HashSet<string> installedFontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            // FontFamily type is not strongly typed; use implicit typing for iteration
            foreach (var family in installedFonts.Families)
            {
                // The family name property is typically 'Name'
                installedFontNames.Add(family.Name);
            }

            // Iterate through all pages and shapes to find text shapes with unsupported fonts
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains any visible text
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.ToString()))
                    {
                        // Collect fonts used in character formatting
                        HashSet<string> shapeFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            if (ch.FontName != null && !string.IsNullOrWhiteSpace(ch.FontName.Value))
                            {
                                shapeFonts.Add(ch.FontName.Value);
                            }
                        }

                        // Determine which fonts are not installed on the system
                        List<string> missingFonts = new List<string>();
                        foreach (string fontName in shapeFonts)
                        {
                            if (!installedFontNames.Contains(fontName))
                            {
                                missingFonts.Add(fontName);
                            }
                        }

                        // Report shapes that use unsupported fonts
                        if (missingFonts.Count > 0)
                        {
                            Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}");
                            Console.WriteLine("  Missing Fonts:");
                            foreach (string missing in missingFonts)
                            {
                                Console.WriteLine($"    {missing}");
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Font validation completed.");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram related errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}