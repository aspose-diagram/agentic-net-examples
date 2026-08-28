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

                // Use the first (default) page
                Page page = diagram.Pages[0];

                // Add two rectangle shapes
                long shapeId1 = page.AddShape(2.0, 5.0, "Rectangle");
                long shapeId2 = page.AddShape(8.0, 5.0, "Rectangle");

                // Retrieve the shape objects (optional, for further modifications)
                Shape shape1 = page.Shapes.GetShape(shapeId1);
                Shape shape2 = page.Shapes.GetShape(shapeId2);

                // Add a dynamic connector shape
                long connectorId = page.AddShape(0.0, 0.0, "Dynamic connector");
                Shape connector = page.Shapes.GetShape(connectorId);

                // Connect the two rectangles using the connector
                page.ConnectShapesViaConnector(
                    shapeId1,
                    ConnectionPointPlace.Bottom,
                    shapeId2,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Set the connector to use a smooth curved (spline) routing
                connector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);

                // Save the diagram to a VSDX file
                diagram.Save("CurvedConnector.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }