using System;
using System.IO;
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

            // Access a shape (example: shape with ID 1 on the first page)
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Set the rotation projection type to a fixed (parallel) projection
            shape.ThreeDFormat.RotationType = new RotationType(RotationTypeValue.Parallel);

            // Apply a 90‑degree counter‑clockwise rotation around the Y‑axis
            shape.ThreeDFormat.RotationYAngle.Value = 90.0;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
