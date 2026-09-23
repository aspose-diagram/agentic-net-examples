using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path for the exported SVG file
            string outputPath = "output.svg";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Apply auto‑spacing to each page
            foreach (Page page in diagram.Pages)
            {
                // Configure spacing distances (in inches)
                AutoSpaceOptions spacingOptions = new AutoSpaceOptions();
                spacingOptions.DistanceInHorizontal = 0.5; // horizontal gap
                spacingOptions.DistanceInVertical = 0.5;   // vertical gap

                // Auto‑space all shapes on the page
                page.AutoSpaceShapes(page.Shapes, spacingOptions);
            }

            // Set SVG export options
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            svgOptions.ExportHiddenPage = false;          // do not export hidden pages
            svgOptions.ExportGuideShapes = false;         // optional: omit guide shapes
            svgOptions.SVGFitToViewPort = true;           // preserve viewbox

            // Save the diagram as SVG
            diagram.Save(outputPath, svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
