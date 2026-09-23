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

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Add two rectangle shapes to the page
            long shape1Id = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
            long shape2Id = diagram.AddShape(5.0, 5.0, "Rectangle", 0);

            // Retrieve the shape objects (optional, for further manipulation)
            Shape shape1 = page.Shapes.GetShape(shape1Id);
            Shape shape2 = page.Shapes.GetShape(shape2Id);

            // Add a dynamic connector shape
            long connectorId = diagram.AddShape(0.0, 0.0, "Dynamic connector", 0);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Connect the two rectangles using the connector
            page.ConnectShapesViaConnector(
                shape1Id,
                ConnectionPointPlace.Bottom,
                shape2Id,
                ConnectionPointPlace.Top,
                connectorId);

            // Set the connector to use a curved (smooth spline) line
            connector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);

            // Save the diagram to a VSDX file
            diagram.Save("CurvedConnector.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
