using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram and output diagram paths
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page
                Page page = diagram.Pages[0];

                // Ensure there are at least two shapes to connect
                if (page.Shapes.Count < 2)
                {
                    Console.Error.WriteLine("The page does not contain enough shapes to perform connection.");
                    return;
                }

                // Retrieve the first two shapes by their IDs
                long shapeId1 = page.Shapes[0].ID;
                long shapeId2 = page.Shapes[1].ID;

                Shape shape1 = page.Shapes.GetShape(shapeId1);
                Shape shape2 = page.Shapes.GetShape(shapeId2);

                // Add a dynamic connector shape to the page (position is arbitrary)
                long connectorId = page.AddShape(1.0, 1.0, "Dynamic connector");
                Shape connectorShape = page.Shapes.GetShape(connectorId);

                // Initial connection using default routing (straight line)
                page.ConnectShapesViaConnector(shape1.ID, ConnectionPointPlace.Right, shape2.ID, ConnectionPointPlace.Left, connectorId);
                Console.WriteLine("Connected shapes with default routing.");

                // Compare routing by changing the connector's routing type
                // 1. Straight lines
                connectorShape.SetConnectorsType(ConnectorsTypeValue.StraightLines);
                Console.WriteLine("Connector routing set to StraightLines.");

                // 2. Right-angle routing
                connectorShape.SetConnectorsType(ConnectorsTypeValue.RightAngle);
                Console.WriteLine("Connector routing set to RightAngle.");

                // 3. Curved lines routing
                connectorShape.SetConnectorsType(ConnectorsTypeValue.CurvedLines);
                Console.WriteLine("Connector routing set to CurvedLines.");

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}