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

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Find two non‑connector shapes on the page
                Shape shape1 = null;
                Shape shape2 = null;
                foreach (Shape s in page.Shapes)
                {
                    if (!s.OneD) // non‑1D shapes are regular shapes
                    {
                        if (shape1 == null)
                            shape1 = s;
                        else if (shape2 == null)
                        {
                            shape2 = s;
                            break;
                        }
                    }
                }

                if (shape1 == null || shape2 == null)
                {
                    Console.WriteLine("Unable to find two regular shapes to connect.");
                    return;
                }

                long shape1Id = shape1.ID;
                long shape2Id = shape2.ID;

                // Add a dynamic connector shape (connector)
                long connectorId = diagram.AddShape(0, 0, "Dynamic connector", 0);
                Shape connector = page.Shapes.GetShape(connectorId);

                // Record the initial routing type of the connector
                ConnectorsTypeValue initialRouting = connector.GetConnectorsType();

                Console.WriteLine($"Initial connector routing: {initialRouting}");

                // Connect the two shapes using the connector
                page.ConnectShapesViaConnector(shape1Id, ConnectionPointPlace.Right,
                                              shape2Id, ConnectionPointPlace.Bottom,
                                              connectorId);

                // Change the connector routing to curved lines
                connector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);
                ConnectorsTypeValue afterRouting = connector.GetConnectorsType();

                Console.WriteLine($"Connector routing after change: {afterRouting}");

                // Re‑connect the same shapes with the updated routing
                // (First remove existing connection by deleting the connector)
                page.Shapes.Remove(connector);
                // Add a new connector for the second connection
                long newConnectorId = diagram.AddShape(0, 0, "Dynamic connector", 0);
                Shape newConnector = page.Shapes.GetShape(newConnectorId);
                newConnector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);

                page.ConnectShapesViaConnector(shape1Id, ConnectionPointPlace.Right,
                                              shape2Id, ConnectionPointPlace.Bottom,
                                              newConnectorId);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
