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

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (or any specific page you need)
                Page page = diagram.Pages[0];

                // Configure auto‑spacing options
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 2, // horizontal spacing in inches
                    DistanceInVertical = 2    // vertical spacing in inches
                };

                // Apply auto‑spacing to all shapes on the page
                page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

                // Configure SVG export options
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    ExportHiddenPage = false,
                    ExportGuideShapes = false,
                    SVGFitToViewPort = true,
                    ExportElementAsRectTag = true
                };

                // Save the diagram as SVG using the configured options
                diagram.Save(outputPath, svgOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }