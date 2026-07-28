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

            // Input and output Visio file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve external URL mapping (replace with real data feed as needed)
            Dictionary<string, string> urlMap = GetUrlMapping();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Use the universal shape name as the key to find a URL
                    if (urlMap.TryGetValue(shape.NameU, out string url) && !string.IsNullOrWhiteSpace(url))
                    {
                        // Ensure the Hyperlinks collection exists
                        if (shape.Hyperlinks == null)
                            continue; // Defensive check

                        // Create a new hyperlink and set its properties
                        Hyperlink link = new Hyperlink();
                        link.Name = "ExternalLink";
                        link.Address.Value = url;
                        link.Description.Value = $"Link to {url}";

                        // Add the hyperlink to the shape
                        shape.Hyperlinks.Add(link);
                    }
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Mock method simulating an external data feed that provides shape‑to‑URL mappings
    static Dictionary<string, string> GetUrlMapping()
    {
        // Replace this with actual data retrieval logic (e.g., HTTP request, database query, etc.)
        return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Rectangle", "https://example.com/rectangle" },
            { "Process",   "https://example.com/process" },
            { "Decision",  "https://example.com/decision" }
        };
    }
}
