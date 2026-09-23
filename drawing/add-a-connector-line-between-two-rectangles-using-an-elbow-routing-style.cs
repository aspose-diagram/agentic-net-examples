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

            // Get the first page (created by default)
            Page page = diagram.Pages[0];

            // Add first rectangle
            // Parameters: pinX, pinY, width, height, master name, isCalculate
            long rect1Id = page.AddShape(2.0, 5.0, 2.0, 1.0, "Rectangle", false);
            Shape rect1 = page.Shapes.GetShape(rect1Id);

            // Add second rectangle
            long rect2Id = page.AddShape(8.0, 5.0, 2.0, 1.0, "Rectangle", false);
            Shape rect2 = page.Shapes.GetShape(rect2Id);

            // Add a dynamic connector shape (size is not relevant for connectors)
            long connectorId = page.AddShape(0.0, 0.0, 0.0, 0.0, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Connect the two rectangles using the connector
            // Use Bottom of the first rectangle and Top of the second rectangle
            page.ConnectShapesViaConnector(
                rect1Id,
                ConnectionPointPlace.Bottom,
                rect2Id,
                ConnectionPointPlace.Top,
                connectorId);

            // Set the connector routing style to elbow (right‑angle)
            connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

            // Save the diagram to a VSDX file
            diagram.Save("ConnectorDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
