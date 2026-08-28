using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Dictionary to store connector counts per page (key = page number)
            Dictionary<int, int> connectorCounts = new Dictionary<int, int>();

            // Iterate through all pages in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // The Connects collection holds a Connect element for each connector on the page
                int connectorCount = page.Connects.Count;

                // Store the count using 1‑based page numbering
                connectorCounts[i + 1] = connectorCount;
            }

            // Example output of the results
            foreach (var kvp in connectorCounts)
            {
                Console.WriteLine($"Page {kvp.Key}: {kvp.Value} connectors");
            }

            // Optionally save the diagram (preserving original content)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
