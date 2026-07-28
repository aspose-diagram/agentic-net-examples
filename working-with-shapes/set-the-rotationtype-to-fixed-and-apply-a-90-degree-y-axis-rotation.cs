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

            // Load an existing Visio diagram (replace with your file path)
            var diagram = new Diagram("input.vsdx");

            // Access the target shape (adjust page index and shape index as needed)
            var shape = diagram.Pages[0].Shapes[1];

            // Set the rotation type to a fixed (parallel) projection
            shape.ThreeDFormat.RotationType = new RotationType(RotationTypeValue.Parallel);

            // Apply a 90‑degree rotation around the Y‑axis.
            // RotationXAngle corresponds to rotation around the Y‑axis.
            shape.ThreeDFormat.RotationXAngle.Value = 90.0;

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
