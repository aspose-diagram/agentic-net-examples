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

                // Add a new page to the diagram
                diagram.Pages.Add(new Page());
                Page page = diagram.Pages[0];

                // Add two rectangle shapes (source and target) using built‑in masters
                // The AddShape method returns the shape ID (long)
                long shape1Id = page.AddShape(2.0, 2.0, "Rectangle");
                long shape2Id = page.AddShape(6.0, 4.0, "Rectangle");

                // Retrieve the shape objects for further manipulation (optional)
                Shape shape1 = page.Shapes.GetShape(shape1Id);
                Shape shape2 = page.Shapes.GetShape(shape2Id);

                // Add a dynamic connector shape
                long connectorId = page.AddShape(4.0, 3.0, "Dynamic connector");
                Shape connector = page.Shapes.GetShape(connectorId);

                // Connect the two rectangles with the connector
                page.ConnectShapesViaConnector(
                    shape1Id,
                    ConnectionPointPlace.Bottom,
                    shape2Id,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Determine a point on the connector segment where the comment should appear.
                // For simplicity, use the midpoint between the two shapes' pins.
                double x1 = shape1.XForm.PinX.Value;
                double y1 = shape1.XForm.PinY.Value;
                double x2 = shape2.XForm.PinX.Value;
                double y2 = shape2.XForm.PinY.Value;

                double commentX = (x1 + x2) / 2.0;
                double commentY = (y1 + y2) / 2.0;

                // Add a comment at the calculated position on the page.
                // This comment is visually attached to the connector segment.
                page.AddComment(commentX, commentY, "Review this connector segment");

                // Optionally, also attach a comment directly to the connector shape.
                // This links the comment to the shape itself.
                page.AddComment(connector, "Connector shape comment");

                // Save the diagram to a VSDX file
                diagram.Save("ConnectorWithComment.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }