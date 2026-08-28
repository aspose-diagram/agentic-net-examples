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

                // Create a new empty Visio diagram
                Diagram diagram = new Diagram();

                // Use the first (default) page
                Page page = diagram.Pages[0];

                // -----------------------------------------------------------------
                // Add first shape (Rectangle) at position (2,2)
                // -----------------------------------------------------------------
                long rectId = page.AddShape(2.0, 2.0, "Rectangle");
                Shape rectShape = page.Shapes.GetShape(rectId);

                // Insert a text field into the rectangle shape
                Field field = new Field();
                field.Value.Val = "Sample Field";
                rectShape.Fields.Add(field);

                // -----------------------------------------------------------------
                // Add second shape (Ellipse) at position (5,5)
                // -----------------------------------------------------------------
                long ellipseId = page.AddShape(5.0, 5.0, "Ellipse");
                Shape ellipseShape = page.Shapes.GetShape(ellipseId);

                // -----------------------------------------------------------------
                // Add a dynamic connector shape (will be used to link the two shapes)
                // -----------------------------------------------------------------
                long connectorId = page.AddShape(3.5, 3.5, "Dynamic connector");
                Shape connectorShape = page.Shapes.GetShape(connectorId);

                // Set connector routing style (optional, e.g., right‑angle routing)
                connectorShape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                // -----------------------------------------------------------------
                // Connect the rectangle to the ellipse using the dynamic connector
                // Connect bottom of rectangle to top of ellipse
                // -----------------------------------------------------------------
                page.ConnectShapesViaConnector(
                    rectId,
                    ConnectionPointPlace.Bottom,
                    ellipseId,
                    ConnectionPointPlace.Top,
                    connectorId);

                // -----------------------------------------------------------------
                // Save the diagram to a VSDX file
                // -----------------------------------------------------------------
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }