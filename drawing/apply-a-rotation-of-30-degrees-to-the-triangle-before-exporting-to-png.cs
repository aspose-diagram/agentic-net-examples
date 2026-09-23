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

            // Assume the diagram has at least one page
            Page page = diagram.Pages[0];

            // Find the first shape that uses the "Triangle" master
            Shape triangleShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Master != null && shape.Master.Name == "Triangle")
                {
                    triangleShape = shape;
                    break;
                }
            }

            if (triangleShape == null)
            {
                throw new Exception("Triangle shape not found in the diagram.");
            }

            // Rotate the triangle by 30 degrees
            triangleShape.SetAngle(30);

            // Prepare PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Export the diagram (with the rotated triangle) to PNG
            diagram.Save("output.png", pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
