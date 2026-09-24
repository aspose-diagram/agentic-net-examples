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

                // Add a rectangle shape (gluing-enabled by default)
                // Parameters: pinX, pinY, master name, isCalculate flag
                long rectShapeId = page.AddShape(2.0, 2.0, "Rectangle", false);
                Shape rectShape = page.Shapes.GetShape(rectShapeId);

                // Add a second rectangle shape to serve as the target
                long targetShapeId = page.AddShape(5.0, 5.0, "Rectangle", false);
                Shape targetShape = page.Shapes.GetShape(targetShapeId);

                // Add a dynamic connector shape
                long connectorId = page.AddShape(0.0, 0.0, "Dynamic connector", false);
                Shape connectorShape = page.Shapes.GetShape(connectorId);

                // Connect the two rectangles via the connector using glue points
                // Use Bottom of the first rectangle and Top of the second rectangle
                page.ConnectShapesViaConnector(
                    rectShapeId,
                    ConnectionPointPlace.Bottom,
                    targetShapeId,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Verify that the connector is attached to both shapes
                int attachmentCount = 0;
                foreach (Connect conn in page.Connects)
                {
                    // One end of the connector
                    if (conn.FromSheet == rectShapeId && conn.ToSheet == connectorId)
                        attachmentCount++;
                    // The other end of the connector
                    if (conn.FromSheet == targetShapeId && conn.ToSheet == connectorId)
                        attachmentCount++;
                }

                if (attachmentCount == 2)
                {
                    Console.WriteLine("Connector successfully attached to both shapes.");
                }
                else
                {
                    throw new Exception("Connector attachment verification failed.");
                }

                // Save the diagram to a VSDX file
                diagram.Save("ConnectorAttachment.vsdx", SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved as ConnectorAttachment.vsdx");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }