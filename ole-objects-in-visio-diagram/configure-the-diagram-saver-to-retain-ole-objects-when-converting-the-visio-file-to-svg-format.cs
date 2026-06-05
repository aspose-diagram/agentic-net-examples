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

            // Load the Visio file
            Diagram diagram = new Diagram("input.vsdx");

            // Create SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Retain OLE objects by embedding them (do not save images separately)
            svgOptions.IsSavingImageSeparately = false;

            // Save the diagram as SVG with the configured options
            diagram.Save("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
