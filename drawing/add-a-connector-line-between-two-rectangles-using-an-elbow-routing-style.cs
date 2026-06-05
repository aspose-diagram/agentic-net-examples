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

                // Add two rectangle shapes using the built‑in "Rectangle" master
                // Parameters: pinX, pinY, master name, page index (0‑based)
                long rect1Id = diagram.AddShape(2.0, 5.0, "Rectangle", 0);
                long rect2Id = diagram.AddShape(6.0, 5.0, "Rectangle", 0);

                // Create a dynamic connector shape
                Shape connectorShape = new Shape();
                long connectorId = diagram.AddShape(connectorShape, "Dynamic connector", 0);

                // Retrieve the connector shape to set its routing style
                Shape connector = page.Shapes.GetShape(connectorId);
                // Set elbow (right‑angle) routing style
                connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                // Connect the two rectangles with the connector
                // Use bottom of the first rectangle and top of the second rectangle
                page.ConnectShapesViaConnector(
                    rect1Id,
                    ConnectionPointPlace.Bottom,
                    rect2Id,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Save the diagram to a VSDX file
                diagram.Save("ConnectorDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }