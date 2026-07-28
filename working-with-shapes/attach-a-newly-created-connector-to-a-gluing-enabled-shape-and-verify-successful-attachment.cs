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

            // Access the first page (created by default)
            Page page = diagram.Pages[0];

            // Add a rectangle shape; gluing is enabled by default
            long rectId = page.AddShape(2.0, 2.0, "Rectangle");
            Shape rect = page.Shapes.GetShape(rectId);

            // Explicitly allow dynamic glue on the rectangle (optional but ensures glue capability)
            rect.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;

            // Add a dynamic connector shape
            long connectorId = page.AddShape(4.0, 4.0, "Dynamic connector");
            Shape connector = page.Shapes.GetShape(connectorId);

            // Connect the rectangle to itself using the connector (glues both ends)
            page.ConnectShapesViaConnector(
                rectId,
                ConnectionPointPlace.Bottom,
                rectId,
                ConnectionPointPlace.Top,
                connectorId);

            // Verify that the rectangle and connector are now connected
            bool isConnected = rect.IsConnected(connector);
            if (isConnected)
            {
                Console.WriteLine("Connector successfully attached to the shape.");
            }
            else
            {
                throw new Exception("Failed to attach connector to the shape.");
            }

            // Save the diagram to a file for visual confirmation
            diagram.Save("ConnectorDemo.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
