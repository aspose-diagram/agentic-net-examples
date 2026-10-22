using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) or default.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output SVG file path (second argument) or default.
        string outputPath = args.Length > 1 ? args[1] : "output.svg";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram.
                for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
                {
                    Page page = diagram.Pages[pageIdx];

                    // Iterate over all shapes on the current page.
                    for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                    {
                        Shape shape = page.Shapes[shapeIdx];

                        // Set connector routing style to orthogonal (right‑angle).
                        // This property is applicable to connector shapes.
                        shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
                    }
                }

                // Configure SVG save options.
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Save the modified diagram as SVG.
                diagram.Save(outputPath, svgOptions);
            }

            Console.WriteLine($"Diagram successfully saved as SVG to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}