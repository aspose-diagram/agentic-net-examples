using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // List to hold connector information for JSON serialization.
                List<ConnectorInfo> connectors = new List<ConnectorInfo>();

                // Iterate through all pages in the diagram.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the current page.
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Identify connector shapes (1-D shapes). shape.OneD returns a native bool.
                        if (shape.OneD)
                        {
                            long connectorId = shape.ID;
                            long fromShapeId = 0;
                            long toShapeId = 0;

                            // Examine the Connects collection to find the start (BeginX) and end (EndX) connections.
                            for (int connIndex = 0; connIndex < page.Connects.Count; connIndex++)
                            {
                                Connect conn = page.Connects[connIndex];

                                // We're only interested in connections where the connector is the source shape.
                                if (conn.FromSheet == connectorId)
                                {
                                    // Determine whether this connection represents the start or end point.
                                    if (string.Equals(conn.FromCell, "BeginX", StringComparison.OrdinalIgnoreCase))
                                    {
                                        fromShapeId = conn.ToSheet;
                                    }
                                    else if (string.Equals(conn.FromCell, "EndX", StringComparison.OrdinalIgnoreCase))
                                    {
                                        toShapeId = conn.ToSheet;
                                    }
                                }
                            }

                            // Add the connector information to the list.
                            connectors.Add(new ConnectorInfo
                            {
                                ConnectorId = connectorId,
                                FromShapeId = fromShapeId,
                                ToShapeId = toShapeId
                            });
                        }
                    }
                }

                // Serialize the connector list to JSON with indentation.
                string json = JsonSerializer.Serialize(connectors, new JsonSerializerOptions { WriteIndented = true });

                // Ensure the output directory exists.
                string outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Write the JSON content to the specified file.
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Connector data exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram operations or file I/O.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Simple DTO for JSON serialization of connector information.
    private class ConnectorInfo
    {
        public long ConnectorId { get; set; }
        public long FromShapeId { get; set; }
        public long ToShapeId { get; set; }
    }
}