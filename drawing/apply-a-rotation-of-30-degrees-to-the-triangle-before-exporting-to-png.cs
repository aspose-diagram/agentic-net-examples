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

            // Load the Visio diagram that contains the triangle
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the triangle shape.
            // Here we assume the triangle is the first user‑defined shape on the first page.
            // Index 0 is the page shape itself, so we start from 1.
            Shape triangle = diagram.Pages[0].Shapes[1];

            // Rotate the triangle by 30 degrees.
            // Shape.SetAngle expects the angle in radians.
            double angleRadians = 30.0 * Math.PI / 180.0;
            triangle.SetAngle(angleRadians);

            // Export the rotated triangle to a PNG file.
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            triangle.ToImage("triangle.png", pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
