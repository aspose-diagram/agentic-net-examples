using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a triangle shape to the first page (page index 0)
            // PinX and PinY are the coordinates (in inches) where the shape will be placed
            // "Triangle" is the master name of the built‑in triangle shape
            diagram.AddShape(4.0, 5.0, "Triangle", 0);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
