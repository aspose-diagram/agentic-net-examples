using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (replace with actual file path)
                string visioPath = "input.vsdx";

                if (!File.Exists(visioPath))
                    throw new FileNotFoundException($"Visio file not found: {visioPath}");

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                    throw new Exception("The diagram contains no pages.");

                // Use the first page
                Page page = diagram.Pages[0];

                // Find the first shape that contains text
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    // Check if shape has non‑empty text
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                    throw new Exception("No shape with text was found in the diagram.");

                // Export the shape to SVG
                string svgOutputPath = "shape.svg";
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                targetShape.ToSvg(svgOutputPath, svgOptions);
                Console.WriteLine($"Shape exported to SVG: {svgOutputPath}");

                // -------------------------------------------------
                // Font validation
                // -------------------------------------------------

                // Enumerate installed system fonts using Aspose.Drawing.Text
                InstalledFontCollection installedFonts = new InstalledFontCollection();

                // Helper to check if a font name exists in the installed collection
                bool IsFontInstalled(string fontName)
                {
                    if (string.IsNullOrWhiteSpace(fontName))
                        return false;

                    // The Families property may be an array; use LINQ to search by name
                    return installedFonts.Families.Any(f => 
                        string.Equals(f.Name, fontName, StringComparison.OrdinalIgnoreCase));
                }

                // 1. Verify that every font used by the diagram is installed
                foreach (Font diagramFont in diagram.Fonts)
                {
                    string fontName = diagramFont.Name;
                    if (!IsFontInstalled(fontName))
                    {
                        throw new Exception($"Diagram uses font '{fontName}' which is not installed on the system.");
                    }
                }

                // 2. Verify that every character run in the target shape retains its original font
                foreach (Aspose.Diagram.Char ch in targetShape.Chars)
                {
                    string fontName = ch.FontName.Value;
                    if (!IsFontInstalled(fontName))
                    {
                        throw new Exception($"Shape contains character with font '{fontName}' which is not installed on the system.");
                    }
                }

                Console.WriteLine("Font validation passed: all fonts used by the shape and diagram are installed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }