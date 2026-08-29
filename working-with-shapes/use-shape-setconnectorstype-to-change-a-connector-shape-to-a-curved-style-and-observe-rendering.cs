using System.IO;
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

            // Path to a Visio stencil that contains the required masters (Dynamic connector, Rectangle)
            // Replace with an actual .vssx/.vss file path on your system.
            string stencilPath = @"C:\Stencils\Basic_U.vssx";

            // Create an empty diagram.
            Diagram diagram = new Diagram();

            // Load the required masters from the stencil into the diagram.
            diagram.AddMaster(stencilPath, "Dynamic connector");
            diagram.AddMaster(stencilPath, "Rectangle");

            // Add two rectangle shapes to the first page (page index 0).
            long rect1Id = diagram.AddShape(2.0, 5.0, "Rectangle", 0);
            long rect2Id = diagram.AddShape(6.0, 5.0, "Rectangle", 0);

            // Add a dynamic connector shape.
            long connectorId = diagram.AddShape(0.0, 0.0, "Dynamic connector", 0);

            // Retrieve the connector shape object.
            Page page = diagram.Pages[0];
            Shape connector = page.Shapes.GetShape(connectorId);

            // Change the connector style to curved.
            connector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);

            // Connect the two rectangles using the curved connector.
            page.ConnectShapesViaConnector(
                rect1Id,
                ConnectionPointPlace.Right,
                rect2Id,
                ConnectionPointPlace.Left,
                connectorId);

            // Save the diagram as a PNG image to observe the curved connector rendering.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save("ConnectorCurved.png", saveOptions);

            Console.WriteLine("Diagram saved as ConnectorCurved.png with a curved connector.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
