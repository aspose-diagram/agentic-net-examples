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
            Diagram diagram = new Diagram("input.vsdx");

            // Select a shape to export – here we take the first shape on the first page
            Shape shape = diagram.Pages[0].Shapes[0];

            // Create SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // The default background for SVG is transparent; no explicit property is needed.
            // If additional options are required, they can be set on svgOptions here.

            // Export the shape to an SVG file using the customized options
            shape.ToSvg("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
