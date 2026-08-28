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
            Diagram diagram = new Diagram("input.vsdx");

            // Locate the triangle shape by its name and rotate it 30 degrees
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == "Triangle")
                    {
                        // Convert 30 degrees to radians (SetAngle expects radians)
                        double angleRadians = Math.PI * 30.0 / 180.0;
                        shape.SetAngle(angleRadians);
                    }
                }
            }

            // Prepare PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Export the rotated triangle shape to a PNG file
            // (Assumes the triangle shape exists; otherwise this block is skipped)
            Shape triangleShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == "Triangle")
                    {
                        triangleShape = shape;
                        break;
                    }
                }
                if (triangleShape != null) break;
            }

            if (triangleShape != null)
            {
                triangleShape.ToImage("triangle.png", pngOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
