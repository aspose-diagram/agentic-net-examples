using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation; // for ConnectionPointPlace enum
using Aspose.Diagram.Saving;      // for SaveFileFormat

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (required)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output directory (optional, defaults to current directory)
        string outputDir = args.Length > 1 ? args[1] : Directory.GetCurrentDirectory();
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // Define the routing styles to compare
        ConnectorsTypeValue[] routingStyles = new ConnectorsTypeValue[]
        {
            ConnectorsTypeValue.StraightLines,
            ConnectorsTypeValue.CurvedLines
        };

        foreach (ConnectorsTypeValue style in routingStyles)
        {
            try
            {
                // Load the diagram fresh for each routing style
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (assumes at least one page exists)
                Page page = diagram.Pages[0];

                // Locate two non‑connector shapes to be linked
                Shape shape1 = null;
                Shape shape2 = null;
                foreach (Shape s in page.Shapes)
                {
                    // Skip 1‑D connector shapes (OneD == true)
                    if (s.OneD) continue;

                    if (shape1 == null)
                        shape1 = s;
                    else if (shape2 == null)
                    {
                        shape2 = s;
                        break;
                    }
                }

                // Ensure we have two shapes to connect
                if (shape1 == null || shape2 == null)
                {
                    Console.Error.WriteLine("Unable to find two non‑connector shapes on the page.");
                    return;
                }

                // Add a dynamic connector shape (master name must exist in the stencil)
                long connectorId = page.AddShape(0, 0, "Dynamic connector", false);
                Shape connector = page.Shapes.GetShape(connectorId);

                // Apply the current routing style to the connector
                connector.SetConnectorsType(style);

                // Connect shape1 (right side) to shape2 (bottom side) using the connector
                page.ConnectShapesViaConnector(
                    shape1.ID,
                    ConnectionPointPlace.Right,
                    shape2.ID,
                    ConnectionPointPlace.Bottom,
                    connectorId);

                // Build output file name that reflects the routing style
                string outFile = Path.Combine(outputDir, $"output_{style}.vsdx");

                // Save the diagram with the applied routing
                diagram.Save(outFile, SaveFileFormat.Vsdx);

                Console.WriteLine($"Saved diagram with routing '{style}' to: {outFile}");
            }
            catch (Exception ex)
            {
                // Log any Aspose.Diagram errors
                Console.Error.WriteLine($"Error processing routing style '{style}': {ex.Message}");
            }
        }
    }
}