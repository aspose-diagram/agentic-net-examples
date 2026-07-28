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

            // Load the diagram that contains the desired theme
            Diagram sourceThemeDiagram = new Diagram("themeDiagram.vsdx");

            // Load the diagram to which the theme will be applied
            Diagram targetDiagram = new Diagram("targetDiagram.vsdx");

            // Apply the theme from the source diagram to the target diagram
            targetDiagram.CopyTheme(sourceThemeDiagram);

            // Configure SVG save options (e.g., export the first page)
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                PageIndex = 0,               // 0‑based index of the page to render
                ExportHiddenPage = false,    // Do not export hidden pages
                ExportGuideShapes = false    // Do not export guide shapes
            };

            // Save the themed diagram as SVG for web preview
            targetDiagram.Save("themedDiagram.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
