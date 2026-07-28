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

            // Path to a Visio stencil that contains the required masters.
            // Replace with an actual .vssx/.vss file path on your system.
            string stencilPath = @"C:\Stencils\basic.vssx";

            // Create a new empty diagram.
            Diagram diagram = new Diagram();

            // Load the required masters (Rectangle and Dynamic connector) from the stencil.
            diagram.AddMaster(stencilPath, "Rectangle");
            diagram.AddMaster(stencilPath, "Dynamic connector");

            // Add two rectangle shapes to the first page.
            long rect1Id = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
            long rect2Id = diagram.AddShape(5.0, 5.0, "Rectangle", 0);

            // Add a dynamic connector shape.
            long connectorId = diagram.AddShape(0.0, 0.0, "Dynamic connector", 0);

            // Retrieve the connector shape object.
            Shape connector = diagram.Pages[0].Shapes.GetShape(connectorId);

            // Change the connector style to curved.
            connector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);

            // Connect the two rectangles using the curved connector.
            diagram.Pages[0].ConnectShapesViaConnector(
                rect1Id,
                ConnectionPointPlace.Bottom,
                rect2Id,
                ConnectionPointPlace.Top,
                connectorId);

            // Export the diagram to PNG to observe the curved connector rendering.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save("CurvedConnector.png", saveOptions);

            Console.WriteLine("Diagram saved as CurvedConnector.png with a curved connector.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
