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

            // Access the first page and retrieve a shape (example: shape with ID 1)
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Apply a rotation of 45 degrees to the shape's XForm
            shape.XForm.Angle.Value = 45.0;

            // Verify that the rotation was set correctly
            if (Math.Abs(shape.XForm.Angle.Value - 45.0) < 0.001)
            {
                Console.WriteLine("Rotation applied successfully.");
            }
            else
            {
                Console.WriteLine("Rotation verification failed.");
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
