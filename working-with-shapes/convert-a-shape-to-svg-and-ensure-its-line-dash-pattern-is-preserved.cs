using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (can be passed as first argument)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output SVG file path
        string outputPath = "shape.svg";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID (example uses ID = 1)
                Shape shape = page.Shapes.GetShape(1);
                if (shape == null)
                {
                    Console.Error.WriteLine("Shape with ID 1 not found on the page.");
                    return;
                }

                // Skip shapes that are logically deleted
                if (shape.Del == BOOL.True)
                {
                    Console.Error.WriteLine("The specified shape is marked as deleted.");
                    return;
                }

                // Set the line dash pattern using the strongly‑typed enum
                // This preserves the dash style when exporting to SVG
                shape.Line.LinePattern.Value = LinePatternValue.Dash;

                // Prepare SVG save options (default configuration)
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Export the shape to an SVG file
                shape.ToSvg(outputPath, svgOptions);

                Console.WriteLine($"Shape successfully exported to SVG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}