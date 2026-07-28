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
            string visioFilePath = @"C:\Docs\sample.vsdx";

            // Path for the exported SVG file
            string svgOutputPath = @"C:\Docs\sample_page.svg";

            // Index of the page to export (0‑based). Change as needed.
            int pageIndexToExport = 2;

            // Load the Visio diagram from file
            Diagram diagram = new Diagram(visioFilePath);

            // Configure SVG export options
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Specify which page to render
                PageIndex = pageIndexToExport,

                // Optional: fit the generated SVG to the viewport
                SVGFitToViewPort = true,

                // Optional: export hidden pages if needed
                ExportHiddenPage = false
            };

            // Save the selected page as SVG using the configured options
            diagram.Save(svgOutputPath, svgOptions);

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
