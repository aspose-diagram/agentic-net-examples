using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and shape identifier (name or ID)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeNameOrId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdentifier = args[1];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Locate the target shape (search across all pages)
            Shape targetShape = null;
            bool identifierIsId = long.TryParse(shapeIdentifier, out long targetId);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    if (identifierIsId && shape.ID == targetId)
                    {
                        targetShape = shape;
                        break;
                    }
                    else if (!identifierIsId && shape.NameU == shapeIdentifier)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape '{shapeIdentifier}' not found in the diagram.");
                return;
            }

            // -----------------------------------------------------------------
            // Collect distinct font names used by the shape's characters
            // -----------------------------------------------------------------
            HashSet<string> usedFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Aspose.Diagram.Char ch in targetShape.Chars)
            {
                string fontName = ch.FontName?.Value;
                if (!string.IsNullOrEmpty(fontName))
                {
                    usedFonts.Add(fontName);
                }
            }

            // -----------------------------------------------------------------
            // Enumerate installed system fonts using Aspose.Drawing.Text
            // -----------------------------------------------------------------
            InstalledFontCollection installedFontsCollection = new InstalledFontCollection();
            HashSet<string> installedFontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var family in installedFontsCollection.Families)
            {
                installedFontNames.Add(family.Name);
            }

            // -----------------------------------------------------------------
            // Verify that each font used by the shape is installed on the system
            // -----------------------------------------------------------------
            bool allFontsInstalled = true;
            foreach (string font in usedFonts)
            {
                if (!installedFontNames.Contains(font))
                {
                    Console.Error.WriteLine($"Warning: Font '{font}' used in the shape is NOT installed on this system.");
                    allFontsInstalled = false;
                }
            }

            if (!allFontsInstalled)
            {
                Console.Error.WriteLine("One or more fonts used by the shape are missing. The exported SVG may not retain original font appearance.");
            }

            // -----------------------------------------------------------------
            // Export the shape to SVG
            // -----------------------------------------------------------------
            string outputDirectory = Path.GetDirectoryName(diagramPath) ?? Directory.GetCurrentDirectory();
            string safeShapeName = string.IsNullOrEmpty(targetShape.NameU) ? "Shape" : targetShape.NameU;
            string svgFileName = $"{safeShapeName}_{targetShape.ID}.svg";
            string svgOutputPath = Path.Combine(outputDirectory, svgFileName);

            SVGSaveOptions svgOptions = new SVGSaveOptions();
            targetShape.ToSvg(svgOutputPath, svgOptions);

            Console.WriteLine($"Shape exported successfully to SVG: {svgOutputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}