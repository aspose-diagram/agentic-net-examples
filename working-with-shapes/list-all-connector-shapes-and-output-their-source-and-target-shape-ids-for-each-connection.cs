using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string inputPath = "input.vsdx";

                // Load the diagram from a file stream
                using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
                {
                    Diagram diagram = new Diagram(stream);

                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Map connector shape ID -> list of shape IDs it is connected to
                        Dictionary<long, List<long>> connectorMap = new Dictionary<long, List<long>>();

                        // Populate the map using the Connects collection
                        foreach (Connect connection in page.Connects)
                        {
                            long connectorId = connection.FromSheet; // The connector shape ID
                            long targetShapeId = connection.ToSheet; // The shape it is glued to

                            if (!connectorMap.ContainsKey(connectorId))
                            {
                                connectorMap[connectorId] = new List<long>();
                            }
                            connectorMap[connectorId].Add(targetShapeId);
                        }

                        // Output information for each connector shape
                        foreach (KeyValuePair<long, List<long>> entry in connectorMap)
                        {
                            long connectorId = entry.Key;
                            List<long> connectedShapeIds = entry.Value;

                            // Retrieve the connector shape to verify it is a 1‑D connector
                            Shape connectorShape = page.Shapes.GetShape(connectorId);
                            if (connectorShape == null || !connectorShape.OneD)
                            {
                                // Not a connector; skip
                                continue;
                            }

                            // Expecting two ends for a typical connector
                            if (connectedShapeIds.Count >= 2)
                            {
                                long sourceId = connectedShapeIds[0];
                                long targetId = connectedShapeIds[1];
                                Console.WriteLine($"Connector ID: {connectorId}, Source Shape ID: {sourceId}, Target Shape ID: {targetId}");
                            }
                            else
                            {
                                // Fallback for connectors with fewer than two connections
                                Console.WriteLine($"Connector ID: {connectorId} has {connectedShapeIds.Count} connected shape(s).");
                                for (int i = 0; i < connectedShapeIds.Count; i++)
                                {
                                    Console.WriteLine($"  Connected Shape {i + 1} ID: {connectedShapeIds[i]}");
                                }
                            }
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }