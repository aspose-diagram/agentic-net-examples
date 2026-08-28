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

            // Load the Visio diagram (lifecycle rule: load)
            var diagram = new Diagram("input.vsdx");

            // Configure SVG save options
            var svgOptions = new SVGSaveOptions
            {
                // Preserve the original viewbox by disabling automatic fit to viewport
                SVGFitToViewPort = false,

                // Export rectangle shapes as <rect> tags so they can be styled via CSS
                ExportElementAsRectTag = true
            };

            // Save the diagram as SVG using the configured options (lifecycle rule: save)
            diagram.Save("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
