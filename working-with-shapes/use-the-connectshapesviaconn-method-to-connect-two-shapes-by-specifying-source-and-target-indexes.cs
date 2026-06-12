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

                // Path to a stencil file that contains the required masters.
                // Replace with the actual path to your .vss stencil.
                string stencilPath = @"C:\Stencils\Basic_U.vss";

                // Load the "Rectangle" master from the stencil.
                diagram.AddMaster(stencilPath, "Rectangle");
                // Load the "Dynamic connector" master from the stencil.
                diagram.AddMaster(stencilPath, "Dynamic connector");

                // Add two rectangle shapes on the first page (page index 0).
                long shapeId1 = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
                long shapeId2 = diagram.AddShape(5.0, 2.0, "Rectangle", 0);

                // Create a connector shape using the "Dynamic connector" master.
                Shape connectorShape = new Shape();
                long connectorId = diagram.AddShape(connectorShape, "Dynamic connector", 0);

                // Connect the two rectangles using the connector.
                // Source shape: shapeId1, connect from its Right connection point.
                // Target shape: shapeId2, connect to its Left connection point.
                diagram.Pages[0].ConnectShapesViaConnector(
                    shapeId1,
                    ConnectionPointPlace.Right,
                    shapeId2,
                    ConnectionPointPlace.Left,
                    connectorId);

                // Save the diagram to a VSDX file.
                diagram.Save("ConnectedDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }