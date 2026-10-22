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
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1‑D shapes). OneD returns a native bool.
                        if (shape.OneD)
                        {
                            long connectorId = shape.ID;

                            // Collect IDs of shapes that are glued to this connector (ends of the connector)
                            List<long> endShapeIds = new List<long>();
                            foreach (Connect conn in page.Connects)
                            {
                                // In the Connect collection, ToSheet is the connector shape ID.
                                if (conn.ToSheet == connectorId)
                                {
                                    endShapeIds.Add(conn.FromSheet);
                                }
                            }

                            // Output the connector information
                            if (endShapeIds.Count >= 2)
                            {
                                // Assume first two entries are the source and target ends
                                Console.WriteLine($"Connector ID: {connectorId}, Source Shape ID: {endShapeIds[0]}, Target Shape ID: {endShapeIds[1]}");
                            }
                            else if (endShapeIds.Count == 1)
                            {
                                Console.WriteLine($"Connector ID: {connectorId}, Single connected Shape ID: {endShapeIds[0]} (incomplete connection)");
                            }
                            else
                            {
                                Console.WriteLine($"Connector ID: {connectorId} has no connections.");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}