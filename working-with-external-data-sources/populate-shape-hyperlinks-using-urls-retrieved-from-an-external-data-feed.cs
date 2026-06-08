using System.IO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    // Entry point of the console application
    static async Task Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            const string inputPath = "input.vsdx";
            // Path where the updated Visio file will be saved
            const string outputPath = "output.vsdx";

            // URL of the external data feed that returns a JSON object:
            // { "ShapeName1": "https://example.com/page1", "ShapeName2": "https://example.org/page2", ... }
            const string dataFeedUrl = "https://your-api.example.com/shape-links";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the mapping of shape names to URLs
            Dictionary<string, string> shapeUrlMap = await GetShapeUrlMapAsync(dataFeedUrl);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Use the universal name (NameU) to look up a URL
                    if (shape.NameU != null && shapeUrlMap.TryGetValue(shape.NameU, out string url))
                    {
                        // Create a new hyperlink instance
                        Hyperlink link = new Hyperlink
                        {
                            Name = "ExternalLink",
                            Address = { Value = url }
                        };

                        // Add the hyperlink to the shape's collection
                        shape.Hyperlinks.Add(link);
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to fetch and deserialize the JSON mapping from the external service
    private static async Task<Dictionary<string, string>> GetShapeUrlMapAsync(string requestUrl)
    {
        using HttpClient client = new HttpClient();

        HttpResponseMessage response = await client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();

        // Deserialize JSON into a dictionary (case‑sensitive keys)
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<Dictionary<string, string>>(json, options)
               ?? new Dictionary<string, string>();
    }
}
