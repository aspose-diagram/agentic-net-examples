using System.IO;
using System;
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

            // Add a shape (e.g., a rectangle) to the first page at position (2, 2)
            // The fourth argument is the page index (0‑based)
            long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);

            // Retrieve the Shape object using the returned ID (cast to int)
            Shape shape = diagram.Pages[0].Shapes.GetShape((int)shapeId);

            // Create a custom connection point
            Connection customConn = new Connection();
            // Set absolute coordinates for the connection point (in inches)
            customConn.X.Ufe.F = "1.2";
            customConn.Y.Ufe.F = "3.4";

            // Add the custom connection point to the shape
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
