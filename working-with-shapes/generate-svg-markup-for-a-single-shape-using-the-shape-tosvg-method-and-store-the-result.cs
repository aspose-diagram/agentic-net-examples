using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ShapeToSvgExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (index 0) and the first shape on that page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Configure SVG save options as needed
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Example option: export rectangle elements as <rect> tags
                ExportElementAsRectTag = true
            };

            // Define the output SVG file path
            string svgFilePath = "shape.svg";

            // Save the selected shape to an SVG file
            shape.ToSvg(svgFilePath, svgOptions);

            Console.WriteLine($"Shape saved as SVG to: {svgFilePath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
