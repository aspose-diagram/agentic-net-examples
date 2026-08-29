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

            // Load an existing Visio diagram file
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve a shape from the page (skip the background shape with ID 1)
            Shape shape = page.Shapes[1];

            // Configure SVG save options if needed
            SVGSaveOptions options = new SVGSaveOptions();
            options.ExportElementAsRectTag = false; // default behavior

            // Save the selected shape as an SVG file
            shape.ToSvg("shape.svg", options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
