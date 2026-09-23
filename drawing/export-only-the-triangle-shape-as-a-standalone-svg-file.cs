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

            // Path to the source Visio file
            string sourcePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Variable to hold the triangle shape when found
            Shape? triangleShape = null;

            // Iterate through all pages and shapes to locate a shape whose master name is "Triangle"
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a master and compare its name
                    if (shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        triangleShape = shape;
                        break;
                    }
                }

                if (triangleShape != null)
                    break;
            }

            // If the triangle shape was not found, abort with an error
            if (triangleShape == null)
                throw new Exception("Triangle shape not found in the diagram.");

            // Export the found triangle shape to a standalone SVG file
            string outputSvgPath = "triangle.svg";
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            triangleShape.ToSvg(outputSvgPath, svgOptions);

            Console.WriteLine($"Triangle shape exported successfully to '{outputSvgPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
