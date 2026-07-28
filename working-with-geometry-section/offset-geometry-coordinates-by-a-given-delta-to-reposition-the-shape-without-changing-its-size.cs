using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Define the shape ID (or use any other method to locate the shape)
            int shapeId = 1; // example shape ID
            Aspose.Diagram.Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Offset values in inches (positive = right/down, negative = left/up)
            double deltaX = 0.5; // move 0.5 inches to the right
            double deltaY = -0.25; // move 0.25 inches up

            // Reposition the shape without altering its size
            shape.Move(deltaX, deltaY);

            // Save the modified diagram
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
