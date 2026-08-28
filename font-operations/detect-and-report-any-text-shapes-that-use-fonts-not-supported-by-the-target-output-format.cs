using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be analyzed
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Retrieve installed system fonts using Aspose.Drawing.Text
                InstalledFontCollection installedFonts = new InstalledFontCollection();

                // Build a set of installed font names for fast lookup
                HashSet<string> installedFontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var family in installedFonts.Families)
                {
                    // FontFamily.Name provides the font name
                    installedFontNames.Add(family.Name);
                }

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

                        // Flag to indicate if the shape uses any unsupported fonts
                        bool hasUnsupportedFont = false;

                        // Iterate over character formatting runs
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            // Font name is stored in the FontName cell
                            string fontName = ch.FontName.Value;

                            // If the font name is empty, skip
                            if (string.IsNullOrWhiteSpace(fontName))
                                continue;

                            // Verify the font is installed on the system
                            if (!installedFontNames.Contains(fontName))
                            {
                                hasUnsupportedFont = true;
                                Console.WriteLine($"Shape ID {shape.ID} (NameU: {shape.NameU}) uses missing font: {fontName}");
                            }
                        }

                        // If the shape has text but no Char entries (e.g., default font), check the default font
                        if (!hasUnsupportedFont && shape.Chars.Count == 0)
                        {
                            // Attempt to get the default font from the shape's text style (if any)
                            // Note: This is a fallback; many shapes rely on the document's default font.
                            // Here we simply report that the shape has text but no explicit font information.
                            Console.WriteLine($"Shape ID {shape.ID} (NameU: {shape.NameU}) contains text but no explicit font information.");
                        }
                    }
                }

                // Optional: Save the diagram after validation (no changes made)
                // diagram.Save("validated_output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }