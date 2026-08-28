using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioToSvgExporter
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputVisioPath = "input.vsdx";

            // Path where the SVG of the selected page will be saved
            string outputSvgPath = "page1.svg";

            // Zero‑based index of the page to export (e.g., 0 for the first page)
            int pageIndexToExport = 0;

            // Load the Visio diagram from file
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // Configure SVG save options
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    // Specify which page to render
                    PageIndex = pageIndexToExport,

                    // Optional: keep hidden pages out of the output
                    ExportHiddenPage = false,

                    // Optional: fit the generated SVG to the viewport
                    SVGFitToViewPort = true
                };

                // Save the selected page as SVG
                diagram.Save(outputSvgPath, svgOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
