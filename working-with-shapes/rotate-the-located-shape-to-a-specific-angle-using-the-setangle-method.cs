using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Locate the shape you want to rotate.
            // Here we assume the shape has ID = 1 on the first page.
            int shapeId = 1;
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Set the desired rotation angle (in degrees).
            double angle = 45.0; // rotate 45 degrees
            shape.SetAngle(angle);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
