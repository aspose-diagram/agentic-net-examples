using System.IO;
using System;
using Aspose.Diagram;

class RotateShapeExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (lifecycle rule: load)
            Diagram diagram = new Diagram("input.vsdx");

            // Select the shape to rotate.
            // Here we take the first user‑drawn shape on the first page (index 1, because index 0 is the page shape).
            Shape shape = diagram.Pages[0].Shapes[1];

            // Desired rotation angle in degrees.
            double angleDegrees = 45.0;

            // Convert degrees to radians because SetAngle expects radians.
            double angleRadians = angleDegrees * Math.PI / 180.0;

            // Rotate the shape by setting its angle (feature rule: Shape.SetAngle).
            shape.SetAngle(angleRadians);

            // Save the modified diagram (lifecycle rule: save).
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
