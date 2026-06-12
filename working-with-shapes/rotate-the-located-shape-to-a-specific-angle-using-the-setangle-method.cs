using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Locate the shape you want to rotate.
            // Here we assume the shape has ID = 1 on the first page.
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Desired rotation angle in degrees.
            double angleDegrees = 45;

            // Convert degrees to radians because SetAngle expects radians.
            double angleRadians = angleDegrees * Math.PI / 180.0;

            // Rotate the shape to the specified angle.
            shape.SetAngle(angleRadians);

            // Save the modified diagram (replace with your desired output path).
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
