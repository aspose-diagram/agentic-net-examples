using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Desired output SVG file path
        string outputPath = "output.svg";

        // Name of the page to export (visible name or universal name)
        string targetPageName = "Page-1";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Find the zero‑based index of the target page
            int pageIndex = -1;
            int currentIndex = 0;
            foreach (Page page in diagram.Pages)
            {
                if (page.Name == targetPageName || page.NameU == targetPageName)
                {
                    pageIndex = currentIndex;
                    break;
                }
                currentIndex++;
            }

            // Abort if the page was not found
            if (pageIndex == -1)
            {
                Console.Error.WriteLine($"Page '{targetPageName}' not found in the diagram.");
                return;
            }

            // Configure SVG export options
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            svgOptions.PageIndex = pageIndex;          // Export only the selected page
            // PageCount is optional; omitting it avoids compatibility issues
            svgOptions.SVGFitToViewPort = true;        // Preserve viewbox for web rendering
            svgOptions.ExportGuideShapes = false;      // Do not export guide shapes
            svgOptions.IsExportComments = false;       // Do not export comments

            // Save the selected page as SVG
            diagram.Save(outputPath, svgOptions);

            Console.WriteLine($"Page '{targetPageName}' has been exported to SVG at '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}