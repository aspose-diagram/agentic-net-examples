using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Desired output SVG file path
            string outputPath = "output.svg";

            // Name of the page to export (adjust as needed)
            string targetPageName = "Page-1";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the index of the target page
            int pageIndex = -1;
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];
                if (page.Name == targetPageName)
                {
                    pageIndex = i;
                    break;
                }
            }

            if (pageIndex == -1)
            {
                throw new Exception($"Page '{targetPageName}' not found in the diagram.");
            }

            // Configure SVG export options
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                PageIndex = pageIndex,
                ExportHiddenPage = false,
                ExportGuideShapes = false,
                SVGFitToViewPort = true,
                ExportElementAsRectTag = true
            };

            // Export the specified page to SVG
            diagram.Save(outputPath, svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
