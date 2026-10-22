using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputSvgFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram (Diagram does not implement IDisposable)
            Diagram diagram = new Diagram(inputPath);

            // ------------------------------------------------------------
            // Apply auto‑spacing to each page in the diagram
            // ------------------------------------------------------------
            AutoSpaceOptions autoSpaceOpts = new AutoSpaceOptions
            {
                DistanceInHorizontal = 2,
                DistanceInVertical = 2
            };

            // Iterate through all pages and auto‑space their shapes
            foreach (Page page in diagram.Pages)
            {
                page.AutoSpaceShapes(page.Shapes, autoSpaceOpts);
            }

            // ------------------------------------------------------------
            // Configure SVG save options
            // ------------------------------------------------------------
            SVGSaveOptions svgOpts = new SVGSaveOptions
            {
                // Preserve the viewbox by fitting the SVG to the viewport
                SVGFitToViewPort = true,
                // Do not export guide shapes
                ExportGuideShapes = false,
                // Do not export comments
                IsExportComments = false,
                // Export only the first page (adjust if needed)
                PageIndex = 0
                // PageCount property is not available in this version; omitted
                // SaveFormat is optional; the Save method infers from options
            };

            // Save the diagram as SVG using the configured options
            diagram.Save(outputPath, svgOpts);
            Console.WriteLine($"Diagram successfully exported to SVG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
