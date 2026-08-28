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

                // Get the first (and only) page
                Page page = diagram.Pages[0];

                // Add first rectangle shape
                // Parameters: PinX, PinY, master name, page index (0)
                long rect1Id = diagram.AddShape(2.0, 5.0, "Rectangle", 0);
                Shape rect1 = page.Shapes.GetShape(rect1Id);
                // Optionally set size (Width, Height) via XForm
                rect1.XForm.Width.Value = 2.0;
                rect1.XForm.Height.Value = 1.0;

                // Add second rectangle shape
                long rect2Id = diagram.AddShape(8.0, 5.0, "Rectangle", 0);
                Shape rect2 = page.Shapes.GetShape(rect2Id);
                rect2.XForm.Width.Value = 2.0;
                rect2.XForm.Height.Value = 1.0;

                // Create a dynamic connector shape
                Shape connectorShape = new Shape();
                long connectorId = diagram.AddShape(connectorShape, "Dynamic connector", 0);
                Shape connector = page.Shapes.GetShape(connectorId);

                // Connect the two rectangles using the connector
                // Use Right side of first rectangle and Left side of second rectangle
                page.ConnectShapesViaConnector(
                    rect1Id,
                    ConnectionPointPlace.Right,
                    rect2Id,
                    ConnectionPointPlace.Left,
                    connectorId);

                // Set the connector routing style to elbow (right-angle)
                connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                // Save the diagram to a VSDX file
                diagram.Save("ConnectorExample.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }