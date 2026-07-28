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

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve a shape from the page (skip the background shape with ID 0)
            Shape shape = page.Shapes[1];

            // Set SVG save options to preserve custom line dash patterns
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                IsSavingCustomLinePattern = true
            };

            // Export the shape to an SVG file
            shape.ToSvg("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
