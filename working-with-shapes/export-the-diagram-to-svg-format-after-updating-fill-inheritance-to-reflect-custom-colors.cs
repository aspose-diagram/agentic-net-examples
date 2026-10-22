using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output SVG file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.svg";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a FillForegnd cell before accessing it
                        if (shape?.Fill?.FillForegnd != null)
                        {
                            // Disable fill inheritance by setting the formula to the explicit color value
                            // This makes the custom fill color independent of any inherited value
                            shape.Fill.FillForegnd.Ufe.F = shape.Fill.FillForegnd.Value;
                        }
                    }
                }

                // Configure SVG save options (only properties that actually exist)
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    // Fit the SVG to the viewport (bool property)
                    SVGFitToViewPort = true,
                    // Do not export guide shapes (bool property)
                    ExportGuideShapes = false,
                    // Do not export comments (bool property)
                    IsExportComments = false,
                    // Export starting from the first page
                    PageIndex = 0
                    // PageCount property omitted because it is not available in this version
                };

                // Save the modified diagram as SVG using the configured options
                diagram.Save(outputPath, svgOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
