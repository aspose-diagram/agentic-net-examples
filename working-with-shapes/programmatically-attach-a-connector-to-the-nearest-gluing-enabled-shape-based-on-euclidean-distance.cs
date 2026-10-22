using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Find the first non‑connector shape to act as the source shape.
            Shape sourceShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes.
                if (shape.Del == BOOL.True)
                    continue;

                // Skip 1‑D connector shapes.
                if (shape.OneD)
                    continue;

                sourceShape = shape;
                break;
            }

            if (sourceShape == null)
            {
                Console.Error.WriteLine("No suitable source shape found on the page.");
                return;
            }

            // Determine the nearest gluing‑enabled shape (non‑connector, not the source).
            Shape nearestShape = null;
            double minDistanceSq = double.MaxValue;

            double sourceX = sourceShape.XForm.PinX.Value;
            double sourceY = sourceShape.XForm.PinY.Value;

            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes.
                if (shape.Del == BOOL.True)
                    continue;

                // Skip connectors and the source shape itself.
                if (shape.OneD || shape.ID == sourceShape.ID)
                    continue;

                // Compute Euclidean distance squared between shape centers.
                double dx = shape.XForm.PinX.Value - sourceX;
                double dy = shape.XForm.PinY.Value - sourceY;
                double distSq = dx * dx + dy * dy;

                if (distSq < minDistanceSq)
                {
                    minDistanceSq = distSq;
                    nearestShape = shape;
                }
            }

            if (nearestShape == null)
            {
                Console.Error.WriteLine("No suitable target shape found for connection.");
                return;
            }

            // Add a dynamic connector shape to the page.
            // Using zero size and position; the connector will be routed automatically.
            long connectorId = page.AddShape(0, 0, 0, 0, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Connect the source shape to the nearest shape using the connector.
            // Use the Center connection point for both shapes.
            page.ConnectShapesViaConnector(
                sourceShape.ID,
                ConnectionPointPlace.Center,
                nearestShape.ID,
                ConnectionPointPlace.Center,
                connectorId);

            // Optionally, set a simple straight line routing style for clarity.
            connector.SetConnectorsType(ConnectorsTypeValue.StraightLines);

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}