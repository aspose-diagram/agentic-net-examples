using System.IO;
using System;
using Aspose.Diagram;

class RotateShapeExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Access a shape on the page.
            // Shape index 1 is typically the first user‑drawn shape (index 0 is the page shape).
            Shape shape = page.Shapes[1];

            // Desired rotation angle in degrees
            double angleInDegrees = 45.0;

            // Convert degrees to radians because XForm.Angle expects radians
            double angleInRadians = angleInDegrees * Math.PI / 180.0;

            // Set the rotation using the XForm.Angle property
            shape.XForm.Angle.Value = angleInRadians;

            // Alternatively, you could use the helper method:
            // shape.SetAngle(angleInRadians);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
