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

            // Path to a stencil that contains the required masters (e.g., "Rectangle" and "Dynamic connector")
            string stencilPath = @"C:\Stencils\basic.vss";

            // Load the diagram with the stencil
            Diagram diagram = new Diagram(stencilPath);

            // Get the first page (default page)
            Page page = diagram.Pages[0];

            // Add two rectangle shapes
            long shapeId1 = page.AddShape(1.0, 1.0, "Rectangle", false);
            long shapeId2 = page.AddShape(3.0, 1.0, "Rectangle", false);

            // Add a dynamic connector shape
            long connectorId = page.AddShape(2.0, 1.0, "Dynamic connector", false);

            // Connect the first rectangle to the second rectangle using the connector
            page.ConnectShapesViaConnector(
                shapeId1,
                ConnectionPointPlace.Right,
                shapeId2,
                ConnectionPointPlace.Left,
                connectorId);

            // Save the resulting diagram
            diagram.Save("ConnectedDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
