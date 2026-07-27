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

                // Add two rectangle shapes
                // Parameters: pinX, pinY, master name, page index
                long rect1Id = diagram.AddShape(2.0, 5.0, "Rectangle", 0);
                long rect2Id = diagram.AddShape(8.0, 5.0, "Rectangle", 0);

                // Add a dynamic connector shape
                Shape connectorShape = new Shape();
                long connectorId = diagram.AddShape(connectorShape, "Dynamic connector", 0);

                // Retrieve the connector shape object for further configuration
                Shape connector = page.Shapes.GetShape(connectorId);

                // Set the connector routing style to elbow (right‑angle)
                connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                // Connect the two rectangles using the connector
                // Use ConnectionPointPlace.Right for the first rectangle and Left for the second
                page.ConnectShapesViaConnector(
                    rect1Id,
                    ConnectionPointPlace.Right,
                    rect2Id,
                    ConnectionPointPlace.Left,
                    connectorId);

                // Save the diagram to a VSDX file
                diagram.Save("ConnectorExample.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }