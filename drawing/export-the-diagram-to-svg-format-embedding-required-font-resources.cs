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

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsd");

            // Create SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Set a default font to ensure Unicode characters are rendered correctly
            svgOptions.DefaultFont = "Arial Unicode MS";

            // Embed images directly into the SVG (do not save them separately)
            svgOptions.IsSavingImageSeparately = false;

            // Make the generated SVG fit to the viewport
            svgOptions.SVGFitToViewPort = true;

            // Optionally, decide whether hidden pages should be exported
            svgOptions.ExportHiddenPage = false;

            // Save the diagram as an SVG file using the configured options
            diagram.Save("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
