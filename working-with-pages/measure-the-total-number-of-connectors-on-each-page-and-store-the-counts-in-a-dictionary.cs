using System.IO;
using Aspose.Diagram;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx"); // replace with your file path

            // Dictionary to store the number of connectors per page (key: page ID, value: connector count)
            Dictionary<long, int> connectorCounts = new Dictionary<long, int>();

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // The Connects collection holds every connector on the page
                int count = page.Connects.Count;
                connectorCounts[page.ID] = count;
            }

            // Example output of the results
            foreach (var kvp in connectorCounts)
            {
                Console.WriteLine($"Page ID {kvp.Key}: {kvp.Value} connectors");
            }

            // If you need to save the diagram after processing, uncomment the line below
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
