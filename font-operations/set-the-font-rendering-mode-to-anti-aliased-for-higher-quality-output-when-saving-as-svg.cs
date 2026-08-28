using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load the diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Create SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // NOTE: SVGSaveOptions does not expose a direct anti‑aliasing property.
            // For raster formats you could use ImageSaveOptions.SmoothingMode = SmoothingMode.HighQuality.
            // Since SVG is a vector format, anti‑aliasing is handled by the viewer/rendering engine.

            // Save the diagram as SVG
            diagram.Save("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
