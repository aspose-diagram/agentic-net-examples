using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Perform auto‑spacing on each page before exporting.
                // ------------------------------------------------------------
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
                // Example spacing values; adjust as needed.
                autoSpaceOptions.DistanceInHorizontal = 0.5;
                autoSpaceOptions.DistanceInVertical = 0.5;

                // Iterate through all pages and apply auto‑spacing.
                foreach (Page page in diagram.Pages)
                {
                    // AutoSpaceShapes expects the shape collection and options.
                    page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
                }

                // ------------------------------------------------------------
                // Configure SVG save options.
                // ------------------------------------------------------------
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                // Ensure the correct format is tracked (optional but explicit).
                svgOptions.SaveFormat = SaveFileFormat.Svg;
                // Example: do not export hidden pages.
                svgOptions.ExportHiddenPage = false;
                // Example: do not export guide shapes.
                svgOptions.ExportGuideShapes = false;
                // Fit the SVG to the viewport (preserves viewbox).
                svgOptions.SVGFitToViewPort = true;

                // ------------------------------------------------------------
                // Save the diagram as SVG.
                // ------------------------------------------------------------
                diagram.Save(outputPath, svgOptions);
                Console.WriteLine($"Diagram successfully exported to SVG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}