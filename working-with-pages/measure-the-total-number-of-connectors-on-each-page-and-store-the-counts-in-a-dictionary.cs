using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as a command-line argument.");
            return;
        }

        string filePath = args[0];

        // Guard against missing file.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(filePath))
            {
                // Dictionary to hold connector counts per page (key = page index, value = count).
                Dictionary<int, int> connectorCounts = new Dictionary<int, int>();

                int pageIndex = 0;

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    int connectorCount = 0;

                    // Iterate through shapes on the page and count 1‑D shapes (connectors).
                    foreach (Shape shape in page.Shapes)
                    {
                        // shape.OneD returns a native bool indicating a connector.
                        if (shape.OneD)
                        {
                            connectorCount++;
                        }
                    }

                    // Store the count for the current page.
                    connectorCounts[pageIndex] = connectorCount;

                    pageIndex++;
                }

                // Output the results.
                Console.WriteLine("Connector counts per page:");
                foreach (KeyValuePair<int, int> kvp in connectorCounts)
                {
                    // Display page numbers starting from 1 for readability.
                    Console.WriteLine($"Page {kvp.Key + 1}: {kvp.Value} connector(s)");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}