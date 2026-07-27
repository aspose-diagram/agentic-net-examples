using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportTriangleToSvg
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram that contains the triangle shape
            Diagram diagram = new Diagram("input.vsd");

            // Configure SVG save options (default options are sufficient for basic export)
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Export the entire diagram (or specific page) to an SVG file
            diagram.Save("triangle.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
