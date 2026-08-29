using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string filePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect IDs of all connector shapes (1‑D shapes) on the current page
                    HashSet<long> connectorIds = new HashSet<long>();
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.OneD) // Connector shapes are 1‑D
                        {
                            connectorIds.Add(shape.ID);
                        }
                    }

                    // If there are no connectors on this page, continue to the next page
                    if (connectorIds.Count == 0)
                        continue;

                    Console.WriteLine($"Page \"{page.Name}\" (ID: {page.ID}) has {connectorIds.Count} connector(s):");

                    // For each connector, find its connections via the page.Connects collection
                    foreach (long connectorId in connectorIds)
                    {
                        // Gather connections where the connector participates
                        List<Connect> relatedConnections = new List<Connect>();
                        foreach (Connect conn in page.Connects)
                        {
                            if (conn.FromSheet == connectorId || conn.ToSheet == connectorId)
                            {
                                relatedConnections.Add(conn);
                            }
                        }

                        // Output source and target shape IDs for each connection of this connector
                        foreach (Connect conn in relatedConnections)
                        {
                            long sourceId = conn.FromSheet == connectorId ? conn.ToSheet : conn.FromSheet;
                            long targetId = connectorId;

                            // Determine direction: if the connector is the FromSheet, then source is the other shape
                            // and target is the connector itself; otherwise reverse.
                            if (conn.FromSheet == connectorId)
                            {
                                sourceId = conn.ToSheet;
                                targetId = conn.FromSheet;
                            }
                            else
                            {
                                sourceId = conn.FromSheet;
                                targetId = conn.ToSheet;
                            }

                            Console.WriteLine($"  Connector ID {connectorId} connects from shape ID {sourceId} to shape ID {targetId}");
                        }

                        // If a connector has no entries in Connects, note it
                        if (relatedConnections.Count == 0)
                        {
                            Console.WriteLine($"  Connector ID {connectorId} has no glued connections.");
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