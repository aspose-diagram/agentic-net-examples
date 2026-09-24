using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, shape ID, output SVG file.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <shapeId> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string shapeIdArg = args[1];
        // Guard: ensure the shape ID can be parsed to a long.
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        string outputPath = args[2];
        // Guard: ensure the directory for the output SVG exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (adjust if needed for multi‑page documents).
            Page page = diagram.Pages[0];

            // Locate the shape by its ID on the page.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                return;
            }

            // Ensure the shape's line dash pattern is set to a dashed style.
            // LinePatternValue.Dash corresponds to a standard dash pattern.
            shape.Line.LinePattern.Value = LinePatternValue.Dash;

            // Create SVG save options; default options preserve line styles.
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Export the specific shape to an SVG file, preserving its dash pattern.
            shape.ToSvg(outputPath, svgOptions);

            Console.WriteLine($"Shape {shapeId} successfully exported to SVG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}