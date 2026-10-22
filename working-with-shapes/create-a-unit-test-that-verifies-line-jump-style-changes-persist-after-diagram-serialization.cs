using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (must exist)
        string inputPath = args.Length > 0 ? args[0] : "sample.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output path for the modified diagram
        string outputPath = "modified_output.vsdx";

        // Variable to hold the connector shape ID for later verification
        long connectorId = 0;

        try
        {
            // Load the original diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the first connector (1‑D shape) that is not deleted
            Shape connectorShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True) continue;

                    // Identify connector shapes (OneD returns a native bool)
                    if (shape.OneD)
                    {
                        connectorShape = shape;
                        break;
                    }
                }
                if (connectorShape != null) break;
            }

            if (connectorShape == null)
            {
                Console.Error.WriteLine("No connector shape found in the diagram.");
                return;
            }

            // Store the shape ID for later verification
            connectorId = connectorShape.ID;

            // Change the line jump style to Arc
            connectorShape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Arc;

            // Save the modified diagram (VSDX format)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during modification: {ex.Message}");
            return;
        }

        try
        {
            // Reload the saved diagram to verify persistence
            Diagram reloadedDiagram = new Diagram(outputPath);

            Shape reloadedConnector = null;
            foreach (Page page in reloadedDiagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True) continue;

                    // Match the shape by its original ID
                    if (shape.ID == connectorId)
                    {
                        reloadedConnector = shape;
                        break;
                    }
                }
                if (reloadedConnector != null) break;
            }

            if (reloadedConnector == null)
            {
                Console.Error.WriteLine("Connector shape not found after reloading.");
                return;
            }

            // Verify that the line jump style persisted as Arc
            var persistedStyle = reloadedConnector.Layout.ConLineJumpStyle.Value;
            if (persistedStyle != ConLineJumpStyleValue.Arc)
            {
                throw new Exception($"Line jump style did not persist. Expected: Arc, Actual: {persistedStyle}");
            }

            Console.WriteLine("Line jump style persisted successfully after serialization.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during verification: {ex.Message}");
        }
    }
}
