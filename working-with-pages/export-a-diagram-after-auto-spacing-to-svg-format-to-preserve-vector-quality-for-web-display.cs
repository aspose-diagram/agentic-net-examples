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

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure auto‑spacing options
                AutoSpaceOptions spaceOptions = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 2, // horizontal spacing in inches
                    DistanceInVertical = 2    // vertical spacing in inches
                };

                // Apply auto‑spacing to each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    page.AutoSpaceShapes(page.Shapes, spaceOptions);
                }

                // Configure SVG export options
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    ExportHiddenPage = false,
                    ExportGuideShapes = false,
                    SVGFitToViewPort = true,
                    ExportElementAsRectTag = true
                };

                // Export the diagram to SVG format
                string outputPath = "output.svg";
                diagram.Save(outputPath, svgOptions);
            }

            Console.WriteLine("Diagram has been auto‑spaced and exported to SVG successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
