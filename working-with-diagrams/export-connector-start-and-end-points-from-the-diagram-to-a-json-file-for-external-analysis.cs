using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramConnectorExport
{
    // DTO for JSON serialization
    public class ConnectorInfo
    {
        public long ConnectorId { get; set; }
        public long StartShapeId { get; set; }
        public long EndShapeId { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // List to hold connector information
                List<ConnectorInfo> connectors = new List<ConnectorInfo>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            long connectorId = shape.ID;

                            // Find connections where this connector is the source (FromSheet)
                            var relatedConnections = page.Connects
                                .Where(c => c.FromSheet == connectorId)
                                .ToList();

                            // Expect exactly two connections: Begin and End
                            long startShapeId = 0;
                            long endShapeId = 0;

                            foreach (var conn in relatedConnections)
                            {
                                // Determine which end based on the cell name
                                if (conn.FromCell != null && conn.FromCell.Contains("Begin", StringComparison.OrdinalIgnoreCase))
                                {
                                    startShapeId = conn.ToSheet;
                                }
                                else if (conn.FromCell != null && conn.FromCell.Contains("End", StringComparison.OrdinalIgnoreCase))
                                {
                                    endShapeId = conn.ToSheet;
                                }
                            }

                            // Fallback: if cell names are not available, assign based on order
                            if (startShapeId == 0 && relatedConnections.Count > 0)
                            {
                                startShapeId = relatedConnections[0].ToSheet;
                            }
                            if (endShapeId == 0 && relatedConnections.Count > 1)
                            {
                                endShapeId = relatedConnections[1].ToSheet;
                            }

                            // Add to the result list
                            connectors.Add(new ConnectorInfo
                            {
                                ConnectorId = connectorId,
                                StartShapeId = startShapeId,
                                EndShapeId = endShapeId
                            });
                        }
                    }
                }

                // Serialize the connector list to JSON
                string jsonOutput = JsonSerializer.Serialize(connectors, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                // Write JSON to file
                string outputPath = "connectors.json";
                File.WriteAllText(outputPath, jsonOutput);

                Console.WriteLine($"Exported {connectors.Count} connectors to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}