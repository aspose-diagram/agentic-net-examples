using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
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

                // Add a star shape at position (2,2)
                long starShapeId = page.AddShape(2.0, 2.0, "Star");

                // Add a rectangle shape at position (5,5)
                long rectShapeId = page.AddShape(5.0, 5.0, "Rectangle");

                // Add a dynamic connector shape (the connector itself)
                long connectorId = page.AddShape(3.5, 3.5, "Dynamic connector");

                // Connect the star to the rectangle using connection point indexes
                // Source index = 1, Target index = 2
                page.ConnectShapesViaConnectorIndex(starShapeId, 1, rectShapeId, 2, connectorId);

                // Save the diagram to a VSDX file
                diagram.Save("ConnectedDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }