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

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output SVG file path
                string outputPath = "output.svg";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Get the first page of the diagram
                    Page page = diagram.Pages[0];

                    // Configure auto‑spacing options
                    AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
                    autoSpaceOptions.DistanceInHorizontal = 2; // horizontal spacing
                    autoSpaceOptions.DistanceInVertical = 2;   // vertical spacing

                    // Apply auto‑spacing to all shapes on the page
                    page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

                    // Configure SVG save options (optional settings)
                    SVGSaveOptions svgOptions = new SVGSaveOptions();
                    svgOptions.ExportHiddenPage = false;
                    svgOptions.ExportGuideShapes = false;
                    svgOptions.SVGFitToViewPort = true;
                    svgOptions.ExportElementAsRectTag = true;

                    // Save the diagram as SVG
                    diagram.Save(outputPath, svgOptions);
                }

                Console.WriteLine($"Diagram exported to SVG at: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }