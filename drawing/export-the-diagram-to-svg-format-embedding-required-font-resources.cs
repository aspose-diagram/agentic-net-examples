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

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsd");

            // Configure SVG save options, including default font for embedding
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Specify a default font to ensure characters are rendered correctly
                DefaultFont = "Arial",

                // Fit the generated SVG to the viewport
                SVGFitToViewPort = true,

                // Export only the first page (change PageIndex for other pages)
                PageIndex = 0,

                // Optional: control additional export behavior
                ExportHiddenPage = false,
                ExportGuideShapes = false,
                IsExportComments = false,
                ExportElementAsRectTag = false,
                EnlargePage = false
            };

            // Save the diagram as an SVG file using the configured options
            diagram.Save("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
