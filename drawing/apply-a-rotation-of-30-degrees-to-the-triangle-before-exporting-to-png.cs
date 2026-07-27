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

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the triangle shape.
            // Here we assume the triangle is the first shape on the first page.
            // Adjust the index or use a different lookup method as needed.
            Shape triangle = diagram.Pages[0].Shapes[1];

            // Apply a rotation of 30 degrees.
            // SetAngle expects the angle in radians.
            double angleInRadians = 30 * Math.PI / 180.0;
            triangle.SetAngle(angleInRadians);

            // Export the rotated shape to PNG.
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            triangle.ToImage("triangle.png", pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
