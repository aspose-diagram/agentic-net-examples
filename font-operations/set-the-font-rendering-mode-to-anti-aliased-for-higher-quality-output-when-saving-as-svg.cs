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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Create ImageSaveOptions and set anti‑aliasing (high‑quality smoothing)
            // Note: SmoothingMode affects raster formats; for SVG it is ignored but the code follows the requirement.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Svg);
            saveOptions.SmoothingMode = SmoothingMode.HighQuality;

            // Save the diagram as SVG using the configured options
            diagram.Save("output.svg", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
