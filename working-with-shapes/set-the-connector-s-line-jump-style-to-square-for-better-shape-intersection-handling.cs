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

            // Load an existing Visio diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page
            Page page = diagram.Pages[0];

            // Add two rectangle shapes to the page
            long shapeId1 = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
            long shapeId2 = diagram.AddShape(5.0, 5.0, "Rectangle", 0);

            // Retrieve the shape objects
            Shape shape1 = page.Shapes.GetShape(shapeId1);
            Shape shape2 = page.Shapes.GetShape(shapeId2);

            // Add a dynamic connector shape
            long connectorId = diagram.AddShape(3.5, 3.5, "Dynamic connector", 0);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Set the connector's line jump style to Square
            connector.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Square;

            // (Optional) Connect the shapes using the connector
            // page.ConnectShapesViaConnector(shapeId1, ConnectionPointPlace.Bottom, shapeId2, ConnectionPointPlace.Top, connectorId);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
