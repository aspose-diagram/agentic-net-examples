using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Variable to hold the connector's ID across the two phases.
        long connectorId = 0;

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Find the first 1‑D connector shape.
                Shape connector = null;

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify a connector (OneD returns a native bool).
                        if (shape.OneD)
                        {
                            connector = shape;
                            connectorId = shape.ID;
                            break;
                        }
                    }

                    if (connector != null)
                        break;
                }

                if (connector == null)
                {
                    Console.Error.WriteLine("No connector shape found in the diagram.");
                    return;
                }

                // Set the line jump style to Arc.
                connector.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Arc;

                // Save the modified diagram to the output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Reload the saved diagram to verify persistence.
            using (Diagram reloaded = new Diagram(outputPath))
            {
                Shape reloadedConnector = null;

                foreach (Page page in reloaded.Pages)
                {
                    // Retrieve the shape by its ID.
                    reloadedConnector = page.Shapes.GetShape(connectorId);
                    if (reloadedConnector != null)
                        break;
                }

                if (reloadedConnector == null)
                {
                    Console.Error.WriteLine("Failed to locate the connector after reload.");
                    return;
                }

                // Verify that the line jump style persisted as Arc.
                var persistedValue = reloadedConnector.Layout.ConLineJumpStyle.Value;
                if (persistedValue != ConLineJumpStyleValue.Arc)
                {
                    throw new Exception($"Line jump style did not persist. Expected: {ConLineJumpStyleValue.Arc}, Actual: {persistedValue}");
                }

                Console.WriteLine("Line jump style change persisted successfully after serialization.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
