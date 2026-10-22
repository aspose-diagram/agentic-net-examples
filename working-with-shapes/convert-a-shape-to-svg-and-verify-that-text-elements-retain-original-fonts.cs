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
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output SVG file path
        string outputSvgPath = "shape_output.svg";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Find the first shape that contains text and is not deleted
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.True)
                    continue; // Skip deleted shapes

                // Check if shape has text
                if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No shape with text found in the diagram.");
                return;
            }

            // Capture original font names used in the shape's text characters
            List<string> originalFontNames = new List<string>();
            for (int i = 0; i < targetShape.Chars.Count; i++)
            {
                // Each Char may have a FontName cell; retrieve its value
                string fontName = targetShape.Chars[i].FontName?.Value;
                if (!string.IsNullOrEmpty(fontName) && !originalFontNames.Contains(fontName))
                {
                    originalFontNames.Add(fontName);
                }
            }

            // Export the shape to SVG using SVGSaveOptions
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            targetShape.ToSvg(outputSvgPath, svgOptions);
            Console.WriteLine($"Shape exported to SVG: {outputSvgPath}");

            // Verify that the fonts used in the shape are installed on the system
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            HashSet<string> installedFontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var family in installedFonts.Families)
            {
                installedFontNames.Add(family.Name);
            }

            bool allFontsInstalled = true;
            foreach (string fontName in originalFontNames)
            {
                if (!installedFontNames.Contains(fontName))
                {
                    Console.Error.WriteLine($"Font not installed on system: {fontName}");
                    allFontsInstalled = false;
                }
            }

            if (allFontsInstalled)
            {
                Console.WriteLine("All fonts used in the shape are installed on the system.");
            }
            else
            {
                Console.Error.WriteLine("One or more fonts used in the shape are missing from the system.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or general exceptions
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}