using System.IO;
using System;
using Aspose.Diagram;

class RotateShapeExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Identify the shape to rotate (example: shape with ID 1 on the first page)
            long shapeId = 1;
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Desired rotation angle in degrees
            double angleDegrees = 45.0;

            // Convert degrees to radians because Shape.SetAngle expects radians
            double angleRadians = angleDegrees * Math.PI / 180.0;

            // Apply the rotation using the Shape.SetAngle method (method rule)
            shape.SetAngle(angleRadians);

            // Keep the XForm.Angle property in sync (property rule)
            shape.XForm.Angle.Value = angleRadians;

            // Save the modified diagram (save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
