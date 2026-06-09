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

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsd");

            // TODO: Update shape geometries here if required
            // Example: diagram.Pages[0].Shapes[1].XForm... (modify as needed)

            // Configure SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Export rectangle shapes as <rect> tags for cleaner SVG
                ExportElementAsRectTag = true,
                // Keep scaling information inside transformation matrices
                IsExportScaleInMatrix = true,
                // Render the first page (0‑based index)
                PageIndex = 0
            };

            // Export the diagram to SVG using the configured options
            diagram.Save("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
