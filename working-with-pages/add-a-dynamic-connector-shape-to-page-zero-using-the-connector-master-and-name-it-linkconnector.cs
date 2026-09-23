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

            // Create a new blank diagram
            Diagram diagram = new Diagram();

            // Get the first page (page index 0)
            Page page = diagram.Pages[0];

            // Add a dynamic connector shape using the master name "Dynamic connector"
            // PinX and PinY are set to arbitrary coordinates (2.0, 2.0)
            long connectorId = page.AddShape(2.0, 2.0, "Dynamic connector", false);

            // Retrieve the connector shape by its ID
            Shape connector = page.Shapes.GetShape(connectorId);

            // Assign a name to the connector shape
            connector.NameU = "LinkConnector";

            // Save the diagram to verify the addition (optional)
            diagram.Save("LinkConnectorDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
