using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx"); // replace with your source file

            // Set up SVG save options to keep hierarchy and styles
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Export the visible page only (default page index is 0)
                PageIndex = 0,

                // Preserve scaling in transformation matrices
                IsExportScaleInMatrix = true,

                // Fit the generated SVG to the viewport
                SVGFitToViewPort = true,

                // Do not export hidden pages, guide shapes, or comments
                ExportHiddenPage = false,
                ExportGuideShapes = false,
                IsExportComments = false,

                // Export elements as standard SVG tags (not forced rect tags)
                ExportElementAsRectTag = false
            };

            // Save the entire diagram as a single SVG file
            diagram.Save("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
