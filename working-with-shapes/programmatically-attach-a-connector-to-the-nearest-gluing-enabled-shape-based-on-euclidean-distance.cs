using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Find the first non‑connector shape to act as the source shape
                long sourceShapeId = -1;
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Skip connector shapes (OneD == true)
                    if (shape.OneD)
                        continue;

                    sourceShapeId = shape.ID;
                    break;
                }

                if (sourceShapeId == -1)
                {
                    Console.Error.WriteLine("No suitable source shape found on the page.");
                    return;
                }

                // Determine the nearest gluing‑enabled shape to the source shape (excluding the source itself)
                Shape sourceShape = page.Shapes.GetShape(sourceShapeId);
                double sourceX = sourceShape.XForm.PinX.Value;
                double sourceY = sourceShape.XForm.PinY.Value;

                long nearestShapeId = -1;
                double minDistanceSq = double.MaxValue;

                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes and the source shape itself
                    if (shape.Del == BOOL.True || shape.ID == sourceShapeId)
                        continue;

                    // Skip connector shapes
                    if (shape.OneD)
                        continue;

                    // Compute Euclidean distance squared between shape centers
                    double dx = shape.XForm.PinX.Value - sourceX;
                    double dy = shape.XForm.PinY.Value - sourceY;
                    double distSq = dx * dx + dy * dy;

                    if (distSq < minDistanceSq)
                    {
                        minDistanceSq = distSq;
                        nearestShapeId = shape.ID;
                    }
                }

                if (nearestShapeId == -1)
                {
                    Console.Error.WriteLine("No suitable target shape found to connect.");
                    return;
                }

                // Add a dynamic connector shape to the page (position will be overridden by the connection)
                long connectorId = page.AddShape(0, 0, 0, 0, "Dynamic connector", false);
                Shape connector = page.Shapes.GetShape(connectorId);

                // Set connector appearance (straight line)
                connector.SetConnectorsType(ConnectorsTypeValue.StraightLines);

                // Connect source shape to nearest shape using the newly created connector
                page.ConnectShapesViaConnector(
                    sourceShapeId,
                    ConnectionPointPlace.Center,
                    nearestShapeId,
                    ConnectionPointPlace.Center,
                    connectorId);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}