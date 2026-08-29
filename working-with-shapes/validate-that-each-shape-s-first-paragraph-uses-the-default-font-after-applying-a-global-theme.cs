using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input diagram and optional theme diagram paths
                string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
                string themePath = args.Length > 1 ? args[1] : null;

                // Load the main diagram
                Diagram diagram = new Diagram(diagramPath);

                // Apply a global theme if a theme file is provided
                if (!string.IsNullOrEmpty(themePath))
                {
                    Diagram themeDiagram = new Diagram(themePath);
                    diagram.CopyTheme(themeDiagram);
                }

                // Set the default font that the theme should enforce
                string defaultFont = "Arial";
                FontConfigs.DefaultFontName = defaultFont;

                bool allValid = true;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Skip shapes without character data (no text)
                        if (shape.Chars.Count == 0)
                            continue;

                        // Retrieve the first character (assumed to belong to the first paragraph)
                        Aspose.Diagram.Char firstChar = shape.Chars[0];
                        string fontName = firstChar.FontName.Value;

                        // Compare with the default font
                        if (!string.Equals(fontName, defaultFont, StringComparison.OrdinalIgnoreCase))
                        {
                            allValid = false;
                            Console.WriteLine($"Shape ID {shape.ID} does not use default font '{defaultFont}'. Found '{fontName}'.");
                        }
                    }
                }

                if (allValid)
                {
                    Console.WriteLine("All shapes' first paragraph use the default font.");
                }
                else
                {
                    throw new Exception("Validation failed: some shapes do not use the default font.");
                }

                // Save the diagram (optional)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }