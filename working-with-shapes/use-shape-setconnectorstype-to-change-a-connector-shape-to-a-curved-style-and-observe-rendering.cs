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

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Add two rectangle shapes to the page
            long rect1Id = page.AddShape(2.0, 2.0, "Rectangle", false);
            long rect2Id = page.AddShape(6.0, 2.0, "Rectangle", false);

            // Retrieve the shape objects for further customization
            Shape rect1 = page.Shapes.GetShape(rect1Id);
            Shape rect2 = page.Shapes.GetShape(rect2Id);

            // Set fill colors so the shapes are visible
            rect1.Fill.FillForegnd.Value = "#FFCC00";
            rect2.Fill.FillForegnd.Value = "#00CCFF";

            // Add a dynamic connector shape (initially straight)
            long connectorId = page.AddShape(0, 0, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Change the connector style to a curved line
            connector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);

            // Connect the two rectangles using the curved connector
            page.ConnectShapesViaConnector(rect1Id, ConnectionPointPlace.Right, rect2Id, ConnectionPointPlace.Left, connectorId);

            // Save the diagram as a PNG image to observe the curved connector
            diagram.Save("ConnectorCurved.png", new ImageSaveOptions(SaveFileFormat.Png));

            Console.WriteLine("Diagram saved with a curved connector.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
