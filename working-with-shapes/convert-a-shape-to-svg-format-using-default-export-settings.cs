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

            // Input diagram file (VDX, VSDX, etc.)
            string diagramPath = "input.vsdx";

            // Output SVG file for the shape
            string svgPath = "shape.svg";

            // Load the diagram from file
            Diagram diagram = new Diagram(diagramPath);

            // Get a shape to export (example: first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[1];

            // Use default SVG export options
            SVGSaveOptions options = new SVGSaveOptions();

            // Export the selected shape to SVG
            shape.ToSvg(svgPath, options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
