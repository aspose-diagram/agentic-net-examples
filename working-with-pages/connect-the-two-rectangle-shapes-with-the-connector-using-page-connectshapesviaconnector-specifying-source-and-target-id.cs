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
                using (Diagram diagram = new Diagram())
                {
                    // Get the default page (index 0)
                    Page page = diagram.Pages[0];

                    // Add the first rectangle shape
                    long rect1Id = diagram.AddShape(2.0, 2.0, "Rectangle", 0);

                    // Add the second rectangle shape
                    long rect2Id = diagram.AddShape(5.0, 5.0, "Rectangle", 0);

                    // Create a connector shape (Dynamic connector)
                    Shape connector = new Shape();
                    long connectorId = diagram.AddShape(connector, "Dynamic connector", 0);

                    // Connect the two rectangles using the connector
                    page.ConnectShapesViaConnector(
                        rect1Id,
                        ConnectionPointPlace.Bottom,
                        rect2Id,
                        ConnectionPointPlace.Top,
                        connectorId);

                    // Optional: set connector routing style to right‑angle
                    Shape connectorShape = page.Shapes.GetShape(connectorId);
                    connectorShape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                    // Save the diagram
                    diagram.Save("ConnectedShapes.vsdx", SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram created and shapes connected successfully.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }