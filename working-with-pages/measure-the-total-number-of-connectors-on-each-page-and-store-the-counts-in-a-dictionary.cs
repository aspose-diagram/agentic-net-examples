using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that an input file path was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found at path '{filePath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required by Aspose.Diagram)
            using (Diagram diagram = new Diagram(filePath))
            {
                // Dictionary to hold connector counts per page (key: Page ID, value: connector count)
                Dictionary<long, int> pageConnectorCounts = new Dictionary<long, int>();

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    int connectorCount = 0;

                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // shape.OneD is a native bool indicating a 1‑D shape (connector)
                        if (shape.OneD)
                        {
                            connectorCount++;
                        }
                    }

                    // Store the count using the page's unique ID
                    pageConnectorCounts[page.ID] = connectorCount;
                }

                // Output the results
                Console.WriteLine("Connector counts per page:");
                foreach (KeyValuePair<long, int> kvp in pageConnectorCounts)
                {
                    Console.WriteLine($"Page ID {kvp.Key}: {kvp.Value} connector(s)");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}