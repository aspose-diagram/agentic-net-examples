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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Set up SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Keep OLE objects embedded in the SVG (do not save them as separate image files)
            svgOptions.IsSavingImageSeparately = false;

            // Specify that the output format is SVG
            svgOptions.SaveFormat = SaveFileFormat.Svg;

            // Save the diagram to SVG using the configured options
            diagram.Save("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
