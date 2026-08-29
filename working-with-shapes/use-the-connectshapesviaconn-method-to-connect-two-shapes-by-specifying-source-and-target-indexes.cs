using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Add two rectangle shapes
                long shapeId1 = page.AddShape(2.0, 5.0, "Rectangle");
                long shapeId2 = page.AddShape(6.0, 5.0, "Rectangle");

                // Add a dynamic connector shape
                long connectorId = page.AddShape(4.0, 5.0, "Dynamic connector");

                // Connect shape 1 to shape 2 using the connector
                // Use ConnectionPointPlace.Right for the source and ConnectionPointPlace.Bottom for the target
                page.ConnectShapesViaConnector(
                    shapeId1,
                    ConnectionPointPlace.Right,
                    shapeId2,
                    ConnectionPointPlace.Bottom,
                    connectorId);

                // Save the diagram to a VSDX file
                diagram.Save("ConnectedDiagram.vsdx", SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram created and shapes connected successfully.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }