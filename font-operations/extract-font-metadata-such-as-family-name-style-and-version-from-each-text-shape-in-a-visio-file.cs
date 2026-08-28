using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the path to the Visio file as an argument.");
            return;
        }

        string visioPath = args[0];
        // Guard against missing file
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Build a lookup of fonts used in the diagram (family name -> Font object)
            Dictionary<string, Font> diagramFontMap = new Dictionary<string, Font>(StringComparer.OrdinalIgnoreCase);
            foreach (Font font in diagram.Fonts)
            {
                // Store each unique font by its family name
                if (!diagramFontMap.ContainsKey(font.Name))
                {
                    diagramFontMap[font.Name] = font;
                }
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Verify the shape contains visible text
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                    {
                        Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");

                        // Enumerate character formatting runs within the shape
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            string familyName = ch.FontName.Value;          // Font family used by this run
                            StyleValue styleEnum = ch.Style.Value;          // Style (Bold, Italic, etc.)

                            // Font version information is not available in Aspose.Diagram; default to "N/A"
                            string versionInfo = "N/A";

                            // If the font exists in the diagram's font collection, we could retrieve additional data here
                            if (diagramFontMap.TryGetValue(familyName, out Font _))
                            {
                                // No version property; keep placeholder
                            }

                            // Output extracted metadata for the character run
                            Console.WriteLine($"  Font Family: {familyName}");
                            Console.WriteLine($"  Style: {styleEnum}");
                            Console.WriteLine($"  Version: {versionInfo}");
                        }

                        // Separator for readability between shapes
                        Console.WriteLine(new string('-', 40));
                    }
                }
            }

            // Optional: Validate that all fonts used in the diagram are installed on the system
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            foreach (Font font in diagram.Fonts)
            {
                bool isInstalled = installedFonts.Families.Any(f => string.Equals(f.Name, font.Name, StringComparison.OrdinalIgnoreCase));
                Console.WriteLine($"Font '{font.Name}' installed on system: {isInstalled}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors encountered during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}