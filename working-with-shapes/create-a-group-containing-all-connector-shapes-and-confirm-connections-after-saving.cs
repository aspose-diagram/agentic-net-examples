using System.IO;
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

            // Work with the first page (index 0)
            Page page = diagram.Pages[0];

            // Add two rectangle shapes
            long rectId1 = page.AddShape(2.0, 5.0, "Rectangle");
            long rectId2 = page.AddShape(6.0, 5.0, "Rectangle");

            // Retrieve the rectangle shapes (optional, for further manipulation)
            Shape rect1 = page.Shapes.GetShape(rectId1);
            Shape rect2 = page.Shapes.GetShape(rectId2);

            // Add three dynamic connectors
            long connId1 = page.AddShape(4.0, 5.0, "Dynamic connector");
            long connId2 = page.AddShape(4.0, 5.0, "Dynamic connector");
            long connId3 = page.AddShape(4.0, 5.0, "Dynamic connector");

            // Connect the rectangles using the connectors
            page.ConnectShapesViaConnector(rectId1, ConnectionPointPlace.Bottom, rectId2, ConnectionPointPlace.Top, connId1);
            page.ConnectShapesViaConnector(rectId1, ConnectionPointPlace.Right, rectId2, ConnectionPointPlace.Left, connId2);
            page.ConnectShapesViaConnector(rectId1, ConnectionPointPlace.Top, rectId2, ConnectionPointPlace.Bottom, connId3);

            // Collect all connector shapes (OneD == true)
            var connectorShapes = new System.Collections.Generic.List<Shape>();
            foreach (Shape shp in page.Shapes)
            {
                if (shp.OneD) // connectors are 1‑D shapes
                {
                    connectorShapes.Add(shp);
                }
            }

            // Group all connector shapes together
            Shape groupShape = page.Shapes.Group(connectorShapes.ToArray());

            // Save the diagram to a VSDX file
            string outputPath = "GroupedConnectors.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Reload the saved diagram
            Diagram loadedDiagram = new Diagram(outputPath);
            Page loadedPage = loadedDiagram.Pages[0];

            // Verify that all connections are still valid
            bool allConnectionsValid = true;
            foreach (Connect conn in loadedPage.Connects)
            {
                try
                {
                    // Attempt to retrieve the source and target shapes
                    Shape fromShape = loadedPage.Shapes.GetShape(conn.FromSheet);
                    Shape toShape = loadedPage.Shapes.GetShape(conn.ToSheet);
                    // If retrieval succeeds, the connection is considered valid
                }
                catch (Exception ex)
                {
                    allConnectionsValid = false;
                    Console.WriteLine($"Invalid connection detected: FromSheet={conn.FromSheet}, ToSheet={conn.ToSheet}. Error: {ex.Message}");
                }
            }

            if (allConnectionsValid)
            {
                Console.WriteLine("All connector connections are valid after saving and reloading.");
            }
            else
            {
                Console.WriteLine("Some connections are invalid after reloading.");
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
