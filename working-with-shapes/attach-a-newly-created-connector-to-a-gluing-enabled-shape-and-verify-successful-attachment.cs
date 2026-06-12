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

                // -----------------------------------------------------------------
                // 1. Add a gluing‑enabled shape (Rectangle) to the page
                // -----------------------------------------------------------------
                long rectShapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
                Shape rectShape = page.Shapes.GetShape(rectShapeId);

                // Enable dynamic glue on the rectangle so connectors can attach to it
                rectShape.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;

                // -----------------------------------------------------------------
                // 2. Add a connector shape (Dynamic connector) to the page
                // -----------------------------------------------------------------
                long connectorId = diagram.AddShape(4.0, 4.0, "Dynamic connector", 0);
                Shape connectorShape = page.Shapes.GetShape(connectorId);

                // -----------------------------------------------------------------
                // 3. Attach the connector to the rectangle
                //    Both ends of the connector are glued to the rectangle for simplicity
                // -----------------------------------------------------------------
                page.ConnectShapesViaConnector(
                    rectShapeId,
                    ConnectionPointPlace.Bottom,
                    rectShapeId,
                    ConnectionPointPlace.Bottom,
                    connectorId);

                // -----------------------------------------------------------------
                // 4. Verify that the connector is glued to the rectangle
                // -----------------------------------------------------------------
                long[] gluedIds = rectShape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);
                bool isAttached = false;
                foreach (long id in gluedIds)
                {
                    if (id == connectorId)
                    {
                        isAttached = true;
                        break;
                    }
                }

                if (isAttached)
                {
                    Console.WriteLine("Connector successfully attached to the gluing-enabled shape.");
                }
                else
                {
                    throw new Exception("Failed to attach the connector to the shape.");
                }

                // Optional: save the diagram to verify visually (output path can be adjusted)
                diagram.Save("ConnectorAttachmentDemo.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }