using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportTriangleShape
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram(@"C:\Path\To\YourDiagram.vsdx");

            // Assume the triangle shape is on the first page
            Page page = diagram.Pages[0];

            // Find the shape whose name indicates it is a triangle
            Shape triangleShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // You may need to adjust the condition based on your diagram's naming
                if (shape.NameU != null && shape.NameU.IndexOf("Triangle", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    triangleShape = shape;
                    break;
                }
            }

            if (triangleShape == null)
            {
                Console.WriteLine("Triangle shape not found in the diagram.");
                return;
            }

            // Prepare SVG save options (default options are sufficient for a single shape)
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Export only the identified triangle shape to a standalone SVG file
            string outputSvgPath = @"C:\Path\To\TriangleShape.svg";
            triangleShape.ToSvg(outputSvgPath, svgOptions);

            Console.WriteLine($"Triangle shape exported successfully to: {outputSvgPath}");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
