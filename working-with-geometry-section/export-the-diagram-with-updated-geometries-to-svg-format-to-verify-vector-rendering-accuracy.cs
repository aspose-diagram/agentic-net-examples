using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output SVG file path
        string outputPath = "output.svg";

        try
        {
            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Update geometry: increase width and height of each shape by 10%
                // -----------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has an XForm (geometry) section
                        if (shape.XForm != null)
                        {
                            // Increase width by 10%
                            double currentWidth = shape.XForm.Width.Value;
                            shape.XForm.Width.Value = currentWidth * 1.10;

                            // Increase height by 10%
                            double currentHeight = shape.XForm.Height.Value;
                            shape.XForm.Height.Value = currentHeight * 1.10;
                        }
                    }
                }

                // -----------------------------------------------------------------
                // Configure SVG save options
                // -----------------------------------------------------------------
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    // Do not export hidden pages
                    ExportHiddenPage = false,
                    // Do not export guide shapes
                    ExportGuideShapes = false,
                    // Fit the SVG to the viewport (preserve viewbox)
                    SVGFitToViewPort = true,
                    // Do not export comments
                    IsExportComments = false,
                    // Export the first page only
                    PageIndex = 0,
                    // Explicitly set the output format (optional but clear)
                    SaveFormat = SaveFileFormat.Svg
                };

                // -----------------------------------------------------------------
                // Save the diagram as SVG using the configured options
                // -----------------------------------------------------------------
                diagram.Save(outputPath, svgOptions);
                Console.WriteLine($"Diagram exported successfully to SVG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
