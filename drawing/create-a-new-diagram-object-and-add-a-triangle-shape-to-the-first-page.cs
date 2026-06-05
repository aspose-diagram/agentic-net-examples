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

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a triangle shape to the first page (page index 0)
            // PinX and PinY define the shape's position on the page
            double pinX = 4.0;
            double pinY = 5.0;
            diagram.AddShape(pinX, pinY, "Triangle", 0);

            // Save the diagram to a VSDX file
            diagram.Save("TriangleDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
