using System.IO;
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

            // Path to a stencil file that contains the required masters.
            // Adjust this path to point to a valid .vss/.vssx file on your system.
            string stencilPath = @"C:\Stencils\Basic_U.vssx";

            // -----------------------------------------------------------------
            // Create first diagram with straight connector routing.
            // -----------------------------------------------------------------
            Diagram diagramStraight = new Diagram();
            // Load masters from the stencil.
            diagramStraight.AddMaster(stencilPath, "Rectangle");
            diagramStraight.AddMaster(stencilPath, "Dynamic connector");

            // Use the first page (index 0).
            Page pageStraight = diagramStraight.Pages[0];

            // Add two rectangle shapes.
            long rect1Id = diagramStraight.AddShape(2.0, 5.0, "Rectangle", 0);
            long rect2Id = diagramStraight.AddShape(8.0, 5.0, "Rectangle", 0);

            // Retrieve shape objects for further manipulation (optional).
            Shape rect1Straight = pageStraight.Shapes.GetShape(rect1Id);
            Shape rect2Straight = pageStraight.Shapes.GetShape(rect2Id);

            // Add a dynamic connector shape.
            long connectorStraightId = diagramStraight.AddShape(0, 0, "Dynamic connector", 0);
            Shape connectorStraight = pageStraight.Shapes.GetShape(connectorStraightId);

            // Set connector routing to straight lines.
            connectorStraight.SetConnectorsType(ConnectorsTypeValue.StraightLines);

            // Connect the two rectangles using the connector.
            pageStraight.ConnectShapesViaConnector(
                rect1Straight.ID,
                ConnectionPointPlace.Right,
                rect2Straight.ID,
                ConnectionPointPlace.Left,
                connectorStraight.ID);

            // Save the diagram to PNG for visual comparison.
            ImageSaveOptions straightOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagramStraight.Save("StraightConnector.png", straightOptions);

            // -----------------------------------------------------------------
            // Create second diagram with curved connector routing.
            // -----------------------------------------------------------------
            Diagram diagramCurved = new Diagram();
            diagramCurved.AddMaster(stencilPath, "Rectangle");
            diagramCurved.AddMaster(stencilPath, "Dynamic connector");

            Page pageCurved = diagramCurved.Pages[0];

            // Add the same rectangle shapes at identical positions.
            long rect1IdC = diagramCurved.AddShape(2.0, 5.0, "Rectangle", 0);
            long rect2IdC = diagramCurved.AddShape(8.0, 5.0, "Rectangle", 0);

            Shape rect1Curved = pageCurved.Shapes.GetShape(rect1IdC);
            Shape rect2Curved = pageCurved.Shapes.GetShape(rect2IdC);

            // Add a dynamic connector shape.
            long connectorCurvedId = diagramCurved.AddShape(0, 0, "Dynamic connector", 0);
            Shape connectorCurved = pageCurved.Shapes.GetShape(connectorCurvedId);

            // Set connector routing to curved lines.
            connectorCurved.SetConnectorsType(ConnectorsTypeValue.CurvedLines);

            // Connect the two rectangles using the connector.
            pageCurved.ConnectShapesViaConnector(
                rect1Curved.ID,
                ConnectionPointPlace.Right,
                rect2Curved.ID,
                ConnectionPointPlace.Left,
                connectorCurved.ID);

            // Save the diagram to PNG for visual comparison.
            ImageSaveOptions curvedOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagramCurved.Save("CurvedConnector.png", curvedOptions);

            // -----------------------------------------------------------------
            // Output simple textual comparison.
            // -----------------------------------------------------------------
            Console.WriteLine("Connector routing comparison:");
            Console.WriteLine($"Straight connector type set to: {connectorStraight.GetConnectorsType()}");
            Console.WriteLine($"Curved connector type set to: {connectorCurved.GetConnectorsType()}");
            Console.WriteLine("Saved diagrams: StraightConnector.png and CurvedConnector.png");
            Console.WriteLine("Open the PNG files to visually compare the routing results.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
