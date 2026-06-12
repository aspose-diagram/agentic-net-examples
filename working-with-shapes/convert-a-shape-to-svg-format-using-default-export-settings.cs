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

            // Retrieve a shape (for example, the first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Initialize SVG save options with default settings
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Export the shape to an SVG file using the default options
            shape.ToSvg("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
