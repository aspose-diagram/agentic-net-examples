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

            // Path to the Visio file
            string filePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Dictionary to store connector counts per page (key: page name, value: count)
            Dictionary<string, int> connectorCounts = new Dictionary<string, int>();

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                int count = 0;

                // Count shapes that are connectors (1‑D shapes)
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.OneD) // true for connector shapes
                    {
                        count++;
                    }
                }

                connectorCounts[page.Name] = count;
            }

            // Output the results
            foreach (var kvp in connectorCounts)
            {
                Console.WriteLine($"Page '{kvp.Key}' has {kvp.Value} connectors.");
            }

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
