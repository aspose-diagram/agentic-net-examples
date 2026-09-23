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

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Add two rectangle shapes
            long rect1Id = page.AddShape(2.0, 2.0, "Rectangle", false);
            long rect2Id = page.AddShape(5.0, 2.0, "Rectangle", false);

            // Add a dynamic connector shape
            long connectorId = page.AddShape(0.0, 0.0, "Dynamic connector", false);

            // Connect the first rectangle to the second rectangle using the connector
            page.ConnectShapesViaConnector(
                rect1Id,
                ConnectionPointPlace.Right,
                rect2Id,
                ConnectionPointPlace.Left,
                connectorId);

            // Save the diagram to a VSDX file
            diagram.Save("ConnectedDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
