using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load the Visio diagram
            var diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Create SVG save options
            var svgOptions = new Aspose.Diagram.Saving.SVGSaveOptions();

            // Retain OLE objects by embedding them (do not save images separately)
            svgOptions.IsSavingImageSeparately = false;

            // Save the diagram as SVG using the configured options
            diagram.Save("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
