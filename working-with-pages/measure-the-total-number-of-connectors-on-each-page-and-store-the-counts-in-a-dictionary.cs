using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Dictionary to store the number of connectors per page (key: page index, value: connector count)
            Dictionary<int, int> connectorCounts = new Dictionary<int, int>();

            // Iterate through all pages in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // The Connects collection contains all connector elements on the page
                int connectorCount = page.Connects.Count;

                // Store the count in the dictionary
                connectorCounts[i] = connectorCount;
            }

            // Example output of the results
            foreach (var kvp in connectorCounts)
            {
                Console.WriteLine($"Page {kvp.Key}: {kvp.Value} connectors");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
