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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Create SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Preserve the original viewbox (do not automatically fit to viewport)
            svgOptions.SVGFitToViewPort = false;

            // Enable CSS styling for shapes (e.g., custom line patterns)
            svgOptions.IsSavingCustomLinePattern = true;

            // Optional: keep images separate to allow CSS referencing
            svgOptions.IsSavingImageSeparately = true;

            // Save the diagram as SVG using the configured options
            diagram.Save("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
