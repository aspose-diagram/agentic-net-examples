using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text; // Required for potential font enumeration

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, output SVG file, shape ID to export
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputSvgPath> <shapeId>");
            return;
        }

        string visioPath = args[0];
        // Guard: ensure the Visio file exists before proceeding
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        string svgPath = args[1];
        string shapeIdArg = args[2];

        // Guard: validate that the shape ID argument can be parsed to a long integer
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram from the provided file path
            Diagram diagram = new Diagram(visioPath);

            // Retrieve the first page (index 0) – adjust if a different page is required
            Page page = diagram.Pages[0];

            // Obtain the shape by its ID; GetShape returns null if the ID is not present
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                return;
            }

            // Collect all distinct font names used by the shape's character runs
            HashSet<string> shapeFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Aspose.Diagram.Char ch in shape.Chars)
            {
                // Guard: ignore empty font entries
                if (!string.IsNullOrWhiteSpace(ch.FontName.Value))
                {
                    shapeFonts.Add(ch.FontName.Value);
                }
            }

            // Export the specific shape to an SVG file using the built‑in ToSvg method
            SVGSaveOptions svgOptions = new SVGSaveOptions(); // default options are sufficient
            shape.ToSvg(svgPath, svgOptions);

            // Read the generated SVG content for verification
            string svgContent = File.ReadAllText(svgPath);

            // Verify that each font used in the shape appears in the SVG output
            foreach (string fontName in shapeFonts)
            {
                // Search for the font name in common SVG font-family attribute patterns
                bool found = svgContent.Contains($"font-family:{fontName}", StringComparison.OrdinalIgnoreCase) ||
                             svgContent.Contains($"font-family=\"{fontName}\"", StringComparison.OrdinalIgnoreCase) ||
                             svgContent.Contains($"font-family='{fontName}'", StringComparison.OrdinalIgnoreCase);

                if (!found)
                {
                    // Report missing font and abort with an exception
                    Console.Error.WriteLine($"Font verification failed: '{fontName}' not found in SVG.");
                    throw new Exception($"Font '{fontName}' missing in exported SVG.");
                }
            }

            // If execution reaches this point, all fonts were successfully verified
            Console.WriteLine("Shape exported to SVG successfully and all fonts were retained.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors and report them to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}