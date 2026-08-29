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

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page at (2.0, 2.0)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");
            Shape shape = page.Shapes.GetShape(shapeId);

            // Create a custom connection point at coordinates (1.2, 3.4)
            Connection customConn = new Connection();
            customConn.X.Ufe.F = "1.2";
            customConn.Y.Ufe.F = "3.4";

            // Add the connection point to the shape
            shape.Connections.Add(customConn);

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
