using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Input Visio file path (replace with actual path)
        string inputPath = "input.vsdx";
        // Desired SVG output file path
        string outputPath = "output.svg";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure SVG export options
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                ExportHiddenPage = false,          // Do not export hidden pages
                ExportGuideShapes = false,         // Exclude guide shapes
                SVGFitToViewPort = true,           // Fit SVG to viewport
                ExportElementAsRectTag = true      // Export shapes as <rect> where appropriate
            };

            // Save the diagram as SVG while preserving hierarchy and styles
            diagram.Save(outputPath, svgOptions);

            Console.WriteLine($"Diagram successfully exported to SVG: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during SVG export: {ex.Message}");
        }
    }
}
