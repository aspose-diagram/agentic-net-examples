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

            // Get the active page
            Page page = diagram.ActivePage;

            // Add the first rectangle shape
            long shapeId1 = page.AddShape(2.0, 5.0, "Rectangle");
            Shape shape1 = page.Shapes.GetShape(shapeId1);

            // Add the second rectangle shape
            long shapeId2 = page.AddShape(8.0, 5.0, "Rectangle");
            Shape shape2 = page.Shapes.GetShape(shapeId2);

            // Add a dynamic connector shape (position is arbitrary)
            long connectorId = page.AddShape(5.0, 5.0, "Dynamic connector");
            Shape connector = page.Shapes.GetShape(connectorId);

            // Set the connector to use a smooth curved line (spline)
            connector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);

            // Connect the two rectangles using the connector
            page.ConnectShapesViaConnector(
                shapeId1, ConnectionPointPlace.Bottom,
                shapeId2, ConnectionPointPlace.Top,
                connectorId);

            // Save the diagram to VSDX format
            diagram.Save("CurvedConnector.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
