using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Add a dynamic connector shape at an arbitrary location
            double connectorPinX = 5.0;
            double connectorPinY = 5.0;
            long connectorId = page.AddShape(connectorPinX, connectorPinY, "Dynamic connector");
            Shape connector = page.Shapes.GetShape(connectorId);

            // Find the nearest shape that allows dynamic glue
            Shape nearestShape = null;
            double minDistance = double.MaxValue;

            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes and connectors (1‑D shapes)
                if (shape.Del == BOOL.True || shape.OneD)
                    continue;

                // Check if the shape's glue type permits dynamic glue
                if (shape.Misc.GlueType.Value != GlueTypeValue.AllowDynamicGlue)
                    continue;

                // Compute Euclidean distance between the connector and the candidate shape
                double dx = connector.XForm.PinX.Value - shape.XForm.PinX.Value;
                double dy = connector.XForm.PinY.Value - shape.XForm.PinY.Value;
                double distance = Math.Sqrt(dx * dx + dy * dy);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestShape = shape;
                }
            }

            if (nearestShape != null)
            {
                // Attach the connector's beginning to the nearest shape (using Bottom of connector, Top of target shape)
                page.ConnectShapesViaConnector(
                    connectorId,
                    ConnectionPointPlace.Bottom,
                    nearestShape.ID,
                    ConnectionPointPlace.Top,
                    connectorId);

                Console.WriteLine($"Connector (ID={connectorId}) attached to shape (ID={nearestShape.ID}) at distance {minDistance:F2}.");
            }
            else
            {
                Console.WriteLine("No gluing‑enabled shape found to attach the connector.");
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
