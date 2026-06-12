using System.IO;
using System;
using Aspose.Diagram;

class VerifyShapeRotation
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Assume we work with the first shape on the first page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Set a new rotation angle (e.g., 45 degrees = PI/4 radians)
            double angleInRadians = Math.PI / 4;
            shape.SetAngle(angleInRadians);

            // Read back the Angle cell value from the shape's XForm
            double actualAngle = shape.XForm.Angle.Value;

            // Output the result for verification
            Console.WriteLine($"Set angle (radians): {angleInRadians}");
            Console.WriteLine($"Read back angle (radians): {actualAngle}");

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
