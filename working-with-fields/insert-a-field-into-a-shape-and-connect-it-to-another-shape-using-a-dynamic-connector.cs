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

                // Add first shape (Rectangle) at position (2,2)
                long shapeId1 = page.AddShape(2.0, 2.0, "Rectangle", false);
                Shape shape1 = page.Shapes.GetShape(shapeId1);

                // Add second shape (Rectangle) at position (6,2)
                long shapeId2 = page.AddShape(6.0, 2.0, "Rectangle", false);
                Shape shape2 = page.Shapes.GetShape(shapeId2);

                // Insert a custom field into the first shape
                Field field = new Field();
                field.Value.Val = "CustomFieldValue";
                shape1.Fields.Add(field);

                // Add a dynamic connector shape (will be used to connect the two rectangles)
                long connectorId = page.AddShape(4.0, 2.0, "Dynamic connector", false);
                Shape connector = page.Shapes.GetShape(connectorId);

                // Connect shape1 (bottom) to shape2 (top) using the dynamic connector
                page.ConnectShapesViaConnector(
                    shapeId1,
                    ConnectionPointPlace.Bottom,
                    shapeId2,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Save the diagram to a VSDX file
                diagram.Save("OutputDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }