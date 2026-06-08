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

                // Add two rectangle shapes (master name "Rectangle")
                long rect1Id = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
                long rect2Id = diagram.AddShape(5.0, 2.0, "Rectangle", 0);

                // Retrieve the shape objects for further manipulation
                Shape rect1 = page.Shapes.GetShape(rect1Id);
                Shape rect2 = page.Shapes.GetShape(rect2Id);

                // Insert a text field into the first rectangle
                Field field = new Field();
                field.Value.Val = "Sample Field";
                rect1.Fields.Add(field);

                // Add a dynamic connector shape (master name "Dynamic connector")
                long connectorId = diagram.AddShape(0.0, 0.0, "Dynamic connector", 0);
                Shape connector = page.Shapes.GetShape(connectorId);

                // Connect the two rectangles using the connector
                page.ConnectShapesViaConnector(
                    rect1Id,
                    ConnectionPointPlace.Right,
                    rect2Id,
                    ConnectionPointPlace.Left,
                    connectorId);

                // Optional: set connector routing style to right‑angle
                connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                // Save the diagram to VSDX format
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }