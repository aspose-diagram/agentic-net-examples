using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            var diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Create SVG save options and explicitly enable exporting hidden pages
            var svgOptions = new Aspose.Diagram.Saving.SVGSaveOptions
            {
                ExportHiddenPage = true // Ensure hidden pages are included in the SVG output
            };

            // Save the diagram as SVG using the configured options
            diagram.Save("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
