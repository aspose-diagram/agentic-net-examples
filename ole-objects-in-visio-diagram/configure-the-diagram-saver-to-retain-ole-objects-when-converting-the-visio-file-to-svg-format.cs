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

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Output SVG file path
            string outputPath = "output.svg";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure SVG save options.
            // By default, OLE objects are retained in the SVG output.
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            // Example: do not export hidden pages (optional)
            svgOptions.ExportHiddenPage = false;

            // Save the diagram as SVG using the configured options
            diagram.Save(outputPath, svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
