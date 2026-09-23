using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
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

            // Add two rectangle shapes to serve as start and end points
            long shapeId1 = page.AddShape(2.0, 5.0, "Rectangle", false);
            long shapeId2 = page.AddShape(8.0, 5.0, "Rectangle", false);

            // Retrieve the shape objects (optional, for further modifications)
            Shape shape1 = page.Shapes.GetShape(shapeId1);
            Shape shape2 = page.Shapes.GetShape(shapeId2);

            // Add a dynamic connector shape
            long connectorId = page.AddShape(5.0, 5.0, "Dynamic connector", false);
            Shape connectorShape = page.Shapes.GetShape(connectorId);

            // Connect the two rectangles using the connector
            page.ConnectShapesViaConnector(
                shapeId1,
                ConnectionPointPlace.Bottom,
                shapeId2,
                ConnectionPointPlace.Top,
                connectorId);

            // Attach a comment directly to the connector shape
            page.AddComment(connectorShape, "Review this connector segment");

            // Save the diagram to a VSDX file
            diagram.Save("ConnectorWithComment.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
