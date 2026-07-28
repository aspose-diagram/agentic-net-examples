using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

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

            // Add a Star shape
            // PinX = 2.0, PinY = 5.0 (position on the page)
            long starShapeId = diagram.AddShape(2.0, 5.0, "Star", 0);
            Shape starShape = page.Shapes.GetShape(starShapeId);

            // Add a Rectangle shape
            // PinX = 6.0, PinY = 5.0
            long rectShapeId = diagram.AddShape(6.0, 5.0, "Rectangle", 0);
            Shape rectShape = page.Shapes.GetShape(rectShapeId);

            // Add a Dynamic Connector shape (used to connect the two shapes)
            long connectorId = diagram.AddShape(0.0, 0.0, "Dynamic connector", 0);
            Shape connectorShape = page.Shapes.GetShape(connectorId);

            // Connect the Star (source) to the Rectangle (target)
            // Source index 1 -> Bottom connection point
            // Target index 2 -> Left connection point
            page.ConnectShapesViaConnector(
                starShapeId,
                ConnectionPointPlace.Bottom,   // source index 1
                rectShapeId,
                ConnectionPointPlace.Left,     // target index 2
                connectorId);

            // Save the diagram to a VSDX file
            diagram.Save("ConnectedShapes.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
