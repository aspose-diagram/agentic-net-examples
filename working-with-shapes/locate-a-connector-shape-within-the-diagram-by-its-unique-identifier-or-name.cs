using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string diagramPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Example identifier or name to locate
            long targetId = 12345;                 // replace with actual ID if known
            string targetName = "MyConnector";     // replace with actual Name or NameU if known

            Shape connectorShape = null;

            // Search each page for the connector
            foreach (Page page in diagram.Pages)
            {
                // Try to locate by unique ID
                if (targetId != 0)
                {
                    Shape shapeById = page.Shapes.GetShape(targetId);
                    if (shapeById != null && shapeById.OneD) // OneD indicates a connector
                    {
                        connectorShape = shapeById;
                        break;
                    }
                }

                // If not found by ID, search by Name/NameU
                foreach (Shape shape in page.Shapes)
                {
                    bool nameMatches = (!string.IsNullOrEmpty(targetName) &&
                                        (shape.Name == targetName || shape.NameU == targetName));

                    if (nameMatches && shape.OneD) // ensure it's a connector
                    {
                        connectorShape = shape;
                        break;
                    }
                }

                if (connectorShape != null)
                    break;
            }

            // Output the result
            if (connectorShape != null)
            {
                Console.WriteLine("Connector shape found:");
                Console.WriteLine($"  ID   : {connectorShape.ID}");
                Console.WriteLine($"  Name : {connectorShape.Name}");
                Console.WriteLine($"  NameU: {connectorShape.NameU}");
            }
            else
            {
                Console.WriteLine("Connector shape not found.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
