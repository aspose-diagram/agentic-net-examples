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
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page and a shape on it (shape ID 1 is used as an example)
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Apply a rotation of 45 degrees to the shape's XForm
            shape.XForm.Angle.Value = 45.0;

            // Verify that the rotation was applied
            double appliedAngle = shape.XForm.Angle.Value;
            Console.WriteLine($"Applied rotation angle: {appliedAngle} degrees");

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
