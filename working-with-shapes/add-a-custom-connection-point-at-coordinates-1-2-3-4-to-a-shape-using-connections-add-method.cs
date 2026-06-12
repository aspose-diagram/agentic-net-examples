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

            // Add a rectangle shape on the first page at (2, 2)
            long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Define a custom connection point at coordinates (1.2, 3.4)
            Connection customConn = new Connection();
            customConn.X.Ufe.F = "1.2";
            customConn.Y.Ufe.F = "3.4";

            // Add the connection point to the shape
            shape.Connections.Add(customConn);

            // Save the diagram to a VSDX file
            diagram.Save("CustomConnection.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with custom connection point.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
