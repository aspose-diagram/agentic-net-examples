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

            // Path to the source Visio diagram
            string sourcePath = "input.vsdx";

            // Path for the exported SVG file
            string outputPath = "output.svg";

            // Load the diagram from file
            Diagram diagram = new Diagram(sourcePath);

            // Apply a preset theme to each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                page.PresetTheme = PresetThemeValue.Bubble;
                page.PresetThemeVariant = PresetThemeVariantValue.Variant2;
            }

            // Configure SVG export options
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            svgOptions.ExportHiddenPage = false;      // Do not export hidden pages
            svgOptions.ExportGuideShapes = false;     // Exclude guide shapes
            svgOptions.SVGFitToViewPort = true;       // Fit SVG to viewport

            // Export the diagram to SVG format
            diagram.Save(outputPath, svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
