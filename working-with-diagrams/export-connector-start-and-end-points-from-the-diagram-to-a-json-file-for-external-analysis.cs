using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // List to hold connector information
                var connectors = new List<ConnectorInfo>();

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1-D shapes)
                        if (shape.OneD)
                        {
                            long connectorId = shape.ID;

                            // Find connections where this connector is the target (ToSheet)
                            var relatedConnections = page.Connects
                                .Where(c => c.ToSheet == connectorId)
                                .ToList();

                            // Expect at least two connections (start and end)
                            if (relatedConnections.Count >= 2)
                            {
                                long startShapeId = relatedConnections[0].FromSheet;
                                long endShapeId = relatedConnections[1].FromSheet;

                                connectors.Add(new ConnectorInfo
                                {
                                    PageId = page.ID,
                                    ConnectorId = connectorId,
                                    StartShapeId = startShapeId,
                                    EndShapeId = endShapeId
                                });
                            }
                        }
                    }
                }

                // Serialize the connector list to JSON with indentation
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(connectors, jsonOptions);

                // Write JSON to the output file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Connector data exported to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // DTO for JSON output
    private class ConnectorInfo
    {
        public int PageId { get; set; }
        public long ConnectorId { get; set; }
        public long StartShapeId { get; set; }
        public long EndShapeId { get; set; }
    }
}