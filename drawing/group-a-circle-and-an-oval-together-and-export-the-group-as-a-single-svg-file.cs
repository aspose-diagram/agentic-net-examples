using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram that contains a circle and an oval
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve the shapes to be grouped (replace the IDs with the actual IDs of your circle and oval)
            Shape circle = page.Shapes.GetShape(1);
            Shape oval   = page.Shapes.GetShape(2);

            // Group the circle and oval together
            Shape group = page.Shapes.Group(new Shape[] { circle, oval });

            // Create SVG save options (default configuration)
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Export the grouped shape as a single SVG file
            group.ToSvg("group.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
