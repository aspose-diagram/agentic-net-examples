using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (example ID = 1)
            Shape shape = page.Shapes.GetShape(1);

            // Apply combined rotations of 30 degrees on X, Y, and Z axes
            shape.ThreeDFormat.RotationXAngle.Value = 30;
            shape.ThreeDFormat.RotationYAngle.Value = 30;
            shape.ThreeDFormat.RotationZAngle.Value = 30;

            // Optional: set rotation type (parallel rotation is typical)
            shape.ThreeDFormat.RotationType.Value = RotationTypeValue.Parallel;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
