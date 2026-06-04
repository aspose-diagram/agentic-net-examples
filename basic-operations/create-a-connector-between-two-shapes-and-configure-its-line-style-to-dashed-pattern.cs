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
            // Parameters: PinX, PinY, master name, page index (0 for first page)
            long shapeId1 = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
            long shapeId2 = diagram.AddShape(5.0, 5.0, "Rectangle", 0);

            // Retrieve the shape objects (optional, useful for further modifications)
            Shape shape1 = page.Shapes.GetShape(shapeId1);
            Shape shape2 = page.Shapes.GetShape(shapeId2);

            // Add a dynamic connector shape (the connector itself)
            long connectorId = diagram.AddShape(0, 0, "Dynamic connector", 0);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Connect the two rectangles using the connector
            // Connect bottom of shape1 to top of shape2
            page.ConnectShapesViaConnector(
                shapeId1,
                ConnectionPointPlace.Bottom,
                shapeId2,
                ConnectionPointPlace.Top,
                connectorId);

            // Set the connector's line style to a dashed pattern
            connector.Line.LinePattern.Value = LinePatternValue.Dash;

            // Save the diagram to a VSDX file
            diagram.Save("ConnectorDemo.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
