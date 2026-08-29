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
            Diagram diagram = new Diagram("sample.vsdx");

            // Access the first page and the first shape on that page (index 1, as index 0 is the page itself)
            Shape shape = diagram.Pages[0].Shapes[1];

            // Define the rotation angle in radians (e.g., 45 degrees = PI/4 radians)
            double angleInRadians = Math.PI / 4;

            // Set the shape's rotation angle using the SetAngle method
            shape.SetAngle(angleInRadians);

            // Read back the angle from the shape's XForm.Angle cell
            double readAngle = shape.XForm.Angle.Value;

            // Output the set and read values for verification
            Console.WriteLine($"Angle set (radians): {angleInRadians}");
            Console.WriteLine($"Angle read from shape (radians): {readAngle}");

            // Optionally, save the modified diagram
            diagram.Save("sample_modified.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
