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

            // Load an existing Visio diagram (assumed to be present)
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve a shape to export (e.g., the first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Create SVG save options.
            // The SVGSaveOptions does not expose a background color property; by default the
            // generated SVG has a transparent background, which satisfies the requirement.
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Export the shape to an SVG file using the customized options
            shape.ToSvg("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
