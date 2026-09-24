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

            // Load an existing Visio diagram (replace with your file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // Export the shape to SVG using default settings
            string outputSvgPath = "shape_output.svg";
            shape.ToSvg(outputSvgPath, new SVGSaveOptions());

            Console.WriteLine($"Shape exported to SVG: {outputSvgPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
