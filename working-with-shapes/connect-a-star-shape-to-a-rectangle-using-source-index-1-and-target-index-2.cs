using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty Visio diagram
                Diagram diagram = new Diagram();

                // Add a Star shape to the first page (page index 0)
                // PinX = 2.0, PinY = 5.0 (position in inches)
                long starShapeId = diagram.AddShape(2.0, 5.0, "Star", 0);

                // Add a Rectangle shape to the same page
                // PinX = 5.0, PinY = 5.0
                long rectangleShapeId = diagram.AddShape(5.0, 5.0, "Rectangle", 0);

                // Add a Dynamic Connector shape (used to link the two shapes)
                // Position is not important; it will be adjusted automatically
                long connectorShapeId = diagram.AddShape(0.0, 0.0, "Dynamic connector", 0);

                // Retrieve the first page to perform the connection
                Page page = diagram.Pages[0];

                // Connect the Star (source) to the Rectangle (target) using
                // source connection index = 1 and target connection index = 2
                page.ConnectShapesViaConnectorIndex(
                    starShapeId,      // source shape ID
                    1,                // source connection index
                    rectangleShapeId, // target shape ID
                    2,                // target connection index
                    connectorShapeId  // connector shape ID
                );

                // Save the diagram to a VSDX file
                diagram.Save("ConnectedDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }