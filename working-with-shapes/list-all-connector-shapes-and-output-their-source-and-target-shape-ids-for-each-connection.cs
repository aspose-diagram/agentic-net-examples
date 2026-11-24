using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
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
                    // Dictionary to collect connections per connector shape ID
                    Dictionary<long, List<long>> connectorMap = new Dictionary<long, List<long>>();

                    // Iterate over all connections on the current page
                    foreach (Connect connect in page.Connects)
                    {
                        // Attempt to retrieve the shape that is the source of the connection (FromSheet)
                        Shape fromShape = null;
                        try
                        {
                            fromShape = page.Shapes.GetShape(connect.FromSheet);
                        }
                        catch
                        {
                            // If the shape cannot be retrieved, skip this connection
                            continue;
                        }

                        // Filter only 1‑D connector shapes that are not deleted
                        if (fromShape != null && fromShape.OneD && fromShape.Del == BOOL.False)
                        {
                            // Ensure an entry exists for this connector ID
                            if (!connectorMap.ContainsKey(connect.FromSheet))
                                connectorMap[connect.FromSheet] = new List<long>();

                            // Store the target shape ID (ToSheet) for this connector
                            connectorMap[connect.FromSheet].Add(connect.ToSheet);
                        }
                    }

                    // Output the collected connector information
                    foreach (var kvp in connectorMap)
                    {
                        long connectorId = kvp.Key;
                        List<long> targets = kvp.Value;

                        // Determine source and target IDs (if available)
                        string sourceId = targets.Count > 0 ? targets[0].ToString() : "N/A";
                        string targetId = targets.Count > 1 ? targets[1].ToString() : "N/A";

                        Console.WriteLine($"Connector Shape ID: {connectorId}, Source Shape ID: {sourceId}, Target Shape ID: {targetId}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}