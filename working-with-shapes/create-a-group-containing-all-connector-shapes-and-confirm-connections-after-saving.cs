using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provided via command line or defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists
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
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect all connector (1‑D) shapes that are not deleted
                    List<Shape> connectorShapes = new List<Shape>();
                    foreach (Shape shape in page.Shapes)
                    {
                        // shape.OneD is a native bool, shape.Del is BOOL
                        if (shape.OneD && shape.Del == BOOL.False)
                        {
                            connectorShapes.Add(shape);
                        }
                    }

                    // If any connectors were found, group them together
                    if (connectorShapes.Count > 0)
                    {
                        Shape[] connectorArray = connectorShapes.ToArray();
                        Shape groupShape = page.Shapes.Group(connectorArray);
                        // Optionally give the group a recognizable name
                        groupShape.Name = "AllConnectorsGroup";
                        Console.WriteLine($"Created group on page '{page.Name}' containing {connectorArray.Length} connectors.");
                    }
                }

                // Save the modified diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }

            // Reload the saved diagram to confirm connections
            using (Diagram savedDiagram = new Diagram(outputPath))
            {
                Console.WriteLine("Verifying connections in the saved diagram:");
                foreach (Page page in savedDiagram.Pages)
                {
                    // Iterate through all connections on the page
                    foreach (Connect connect in page.Connects)
                    {
                        Console.WriteLine($"Page '{page.Name}': Connector from shape ID {connect.FromSheet} to shape ID {connect.ToSheet}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}