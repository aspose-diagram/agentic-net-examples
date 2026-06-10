using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Create a new empty diagram (contains a default page)
                using (Diagram diagram = new Diagram())
                {
                    // Get the first (default) page
                    Page page = diagram.Pages[0];

                    // Add two rectangle shapes and capture their IDs (type long)
                    long rect1Id = page.AddShape(2.0, 2.0, "Rectangle");
                    long rect2Id = page.AddShape(5.0, 5.0, "Rectangle");

                    // Add a dynamic connector shape and capture its ID
                    long connectorId = page.AddShape(3.5, 3.5, "Dynamic connector");

                    // Connect the first rectangle to the second rectangle using the connector
                    // Use Bottom of the source shape and Top of the target shape as connection points
                    page.ConnectShapesViaConnector(
                        rect1Id,
                        ConnectionPointPlace.Bottom,
                        rect2Id,
                        ConnectionPointPlace.Top,
                        connectorId);

                    // Optional: set connector routing style (e.g., right‑angle)
                    // Retrieve the connector shape to modify its layout
                    Shape connectorShape = page.Shapes.GetShape(connectorId);
                    connectorShape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                    // Save the diagram to a VSDX file
                    diagram.Save("ConnectedDiagram.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }