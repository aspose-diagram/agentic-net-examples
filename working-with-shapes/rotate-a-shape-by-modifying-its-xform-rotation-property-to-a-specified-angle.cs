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
            var diagram = new Diagram(@"input.vsdx");

            // Access the first page (adjust index as needed)
            var page = diagram.Pages[0];

            // Access the shape you want to rotate (adjust index or use a search)
            var shape = page.Shapes[0];

            // Desired rotation angle in degrees
            double angleDegrees = 45;

            // Convert degrees to radians because the API expects radians
            double angleRadians = angleDegrees * Math.PI / 180.0;

            // Rotate the shape using the SetAngle method (angle in radians)
            shape.SetAngle(angleRadians);

            // Alternatively, you could set the XForm.Angle property directly:
            // shape.XForm.Angle = new DoubleValue(angleRadians);

            // Save the modified diagram
            diagram.Save(@"output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
