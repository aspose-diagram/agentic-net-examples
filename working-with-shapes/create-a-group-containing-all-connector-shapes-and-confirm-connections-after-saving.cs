using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the source diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate each page to collect connector shapes and group them
                foreach (Page page in diagram.Pages)
                {
                    List<Shape> connectorShapes = new List<Shape>();

                    // Gather all 1‑D connector shapes that are not deleted
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Del == BOOL.True)
                            continue; // Skip deleted shapes

                        // OneD is a native bool – true means a connector
                        if (shape.OneD)
                        {
                            connectorShapes.Add(shape);
                        }
                    }

                    // Create a group if any connectors were found
                    if (connectorShapes.Count > 0)
                    {
                        Shape[] array = connectorShapes.ToArray();
                        Shape groupShape = page.Shapes.Group(array);
                        Console.WriteLine($"Created group (ID: {groupShape.ID}) for {array.Length} connector(s) on page '{page.Name}'.");
                    }
                }

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }

            // Reload the saved diagram to verify that all connections are still valid
            using (Diagram savedDiagram = new Diagram(outputPath))
            {
                bool allConnectionsValid = true;
                int totalConnections = 0;
                int brokenConnections = 0;

                foreach (Page page in savedDiagram.Pages)
                {
                    foreach (Connect connect in page.Connects)
                    {
                        totalConnections++;

                        // Retrieve the source and target shapes by their IDs
                        Shape fromShape = page.Shapes.GetShape(connect.FromSheet);
                        Shape toShape = page.Shapes.GetShape(connect.ToSheet);

                        bool fromValid = fromShape != null && fromShape.Del == BOOL.False;
                        bool toValid = toShape != null && toShape.Del == BOOL.False;

                        if (!fromValid || !toValid)
                        {
                            brokenConnections++;
                            Console.Error.WriteLine($"Broken connection on page '{page.Name}': FromSheet={connect.FromSheet}, ToSheet={connect.ToSheet}");
                            allConnectionsValid = false;
                        }
                    }
                }

                Console.WriteLine($"Total connections checked: {totalConnections}");
                Console.WriteLine($"Broken connections found: {brokenConnections}");

                if (!allConnectionsValid)
                {
                    throw new Exception("One or more connections are invalid after saving.");
                }
                else
                {
                    Console.WriteLine("All connections are valid after saving.");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}