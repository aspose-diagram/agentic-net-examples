using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramConnectorExport
{
    // DTO for JSON serialization
    public class ConnectorInfo
    {
        public long FromShapeId { get; set; }
        public long ToShapeId { get; set; }
    }

    public class Program
    {
        // args[0] = input Visio file path, args[1] = output JSON file path
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramConnectorExport <input.vsdx> <output.json>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                var connectors = new List<ConnectorInfo>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Each Connect element represents a link between two shapes
                    foreach (Connect connection in page.Connects)
                    {
                        // FromSheet and ToSheet hold the IDs of the connected shapes
                        connectors.Add(new ConnectorInfo
                        {
                            FromShapeId = connection.FromSheet,
                            ToShapeId = connection.ToSheet
                        });
                    }
                }

                // Serialize to JSON with indentation for readability
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(connectors, jsonOptions);

                // Write JSON to the specified file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Connector data exported to: {outputPath}");
            }
        }
    }
}