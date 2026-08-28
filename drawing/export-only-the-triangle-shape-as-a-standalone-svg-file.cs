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

            // Input Visio file containing the triangle shape
            string inputPath = "input.vsdx";

            // Output SVG file for the triangle shape
            string outputPath = "triangle.svg";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            Shape triangleShape = null;

            // Search for a shape whose master name is "Triangle"
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False && shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        triangleShape = shape;
                        break;
                    }
                }
                if (triangleShape != null)
                    break;
            }

            if (triangleShape == null)
                throw new Exception("Triangle shape not found in the diagram.");

            // Export the found triangle shape to a standalone SVG file
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            triangleShape.ToSvg(outputPath, svgOptions);

            Console.WriteLine($"Triangle shape exported successfully to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
