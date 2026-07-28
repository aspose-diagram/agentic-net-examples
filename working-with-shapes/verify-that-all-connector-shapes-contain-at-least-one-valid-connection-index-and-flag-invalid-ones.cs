using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file to be validated.
                // Replace with the actual file path as needed.
                string filePath = "input.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                bool anyInvalid = false;

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Collect IDs of all connector shapes (1‑D shapes) on the current page.
                    var connectorIds = new System.Collections.Generic.HashSet<long>();
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.OneD) // Connector shapes are 1‑D.
                        {
                            connectorIds.Add(shape.ID);
                        }
                    }

                    // Count how many connections each connector participates in.
                    var connectionCount = new System.Collections.Generic.Dictionary<long, int>();
                    foreach (Connect conn in page.Connects)
                    {
                        long from = conn.FromSheet;
                        long to = conn.ToSheet;

                        if (connectorIds.Contains(from))
                        {
                            if (!connectionCount.ContainsKey(from))
                                connectionCount[from] = 0;
                            connectionCount[from]++;
                        }

                        if (connectorIds.Contains(to))
                        {
                            if (!connectionCount.ContainsKey(to))
                                connectionCount[to] = 0;
                            connectionCount[to]++;
                        }
                    }

                    // Flag connectors that have zero connections.
                    foreach (long connectorId in connectorIds)
                    {
                        int count = connectionCount.ContainsKey(connectorId) ? connectionCount[connectorId] : 0;
                        if (count == 0)
                        {
                            anyInvalid = true;
                            Console.WriteLine($"Invalid connector shape ID {connectorId} on page '{page.Name}' (no connections).");
                        }
                    }
                }

                if (!anyInvalid)
                {
                    Console.WriteLine("All connector shapes have at least one valid connection.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }