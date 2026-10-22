using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (use arguments if provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

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
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page
                Page page = diagram.Pages[0];

                // Find two non‑connector shapes to connect
                Shape shapeFrom = null;
                Shape shapeTo = null;
                foreach (Shape s in page.Shapes)
                {
                    // Skip connector shapes (OneD == true) and deleted shapes
                    if (s.OneD || s.Del == BOOL.True)
                        continue;

                    if (shapeFrom == null)
                        shapeFrom = s;
                    else if (shapeTo == null)
                    {
                        shapeTo = s;
                        break;
                    }
                }

                if (shapeFrom == null || shapeTo == null)
                {
                    Console.Error.WriteLine("Unable to find two suitable shapes to connect.");
                    return;
                }

                // Add a dynamic connector shape to the page
                // Position (0,0) is arbitrary; the connector will be glued later
                long connectorId = page.AddShape(0, 0, "Dynamic connector");
                Shape connectorShape = page.Shapes.GetShape(connectorId);

                // Record the initial routing style of the connector (if any)
                var initialJumpStyle = connectorShape.Layout?.ConLineJumpStyle?.Value;

                // First connection using connection point indexes 1 and 2
                page.ConnectShapesViaConnectorIndex(shapeFrom.ID, 1, shapeTo.ID, 2, connectorId);

                // Record the routing style after the first connection
                var afterFirstJumpStyle = connectorShape.Layout?.ConLineJumpStyle?.Value;

                // Re‑connect using different connection point indexes to observe routing change
                page.ConnectShapesViaConnectorIndex(shapeFrom.ID, 3, shapeTo.ID, 4, connectorId);

                // Record the routing style after the second connection
                var afterSecondJumpStyle = connectorShape.Layout?.ConLineJumpStyle?.Value;

                // Output comparison results
                Console.WriteLine("Connector routing (ConLineJumpStyle) values:");
                Console.WriteLine($"  Initial: {(initialJumpStyle.HasValue ? initialJumpStyle.Value.ToString() : "N/A")}");
                Console.WriteLine($"  After first connect: {(afterFirstJumpStyle.HasValue ? afterFirstJumpStyle.Value.ToString() : "N/A")}");
                Console.WriteLine($"  After second connect: {(afterSecondJumpStyle.HasValue ? afterSecondJumpStyle.Value.ToString() : "N/A")}");

                // Simple comparison logic – report if the routing style changed
                if (afterFirstJumpStyle != afterSecondJumpStyle)
                {
                    Console.WriteLine("Routing style changed after reconnection.");
                }
                else
                {
                    Console.WriteLine("Routing style remained the same after reconnection.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}