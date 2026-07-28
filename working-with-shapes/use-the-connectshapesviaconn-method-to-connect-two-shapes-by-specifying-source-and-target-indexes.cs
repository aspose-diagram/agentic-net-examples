using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

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

            // Add two rectangle shapes on the page
            // Parameters: PinX, PinY, master name, page index (0 = first page)
            long shapeId1 = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
            long shapeId2 = diagram.AddShape(5.0, 2.0, "Rectangle", 0);

            // Add a dynamic connector shape (the line that will link the rectangles)
            long connectorId = diagram.AddShape(0, 0, "Dynamic connector", 0);

            // Connect the first rectangle's right connection point to the second rectangle's left point
            page.ConnectShapesViaConnector(
                shapeId1,
                ConnectionPointPlace.Right,
                shapeId2,
                ConnectionPointPlace.Left,
                connectorId);

            // Save the resulting diagram (optional)
            diagram.Save("ConnectedDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
