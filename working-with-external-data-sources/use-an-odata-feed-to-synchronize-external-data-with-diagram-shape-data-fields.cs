using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Aspose.Diagram;

namespace ODataDiagramSync
{
    // Simple POCO representing an OData entry
    public class ODataItem
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }

    public class Program
    {
        // Entry point – async to allow HTTP calls
        public static async Task Main(string[] args)
        {
            // Validate arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: ODataDiagramSync <VisioFilePath> <ODataEndpointUrl>");
                return;
            }

            string visioPath = args[0];
            string odataUrl = args[1];

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(visioPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Retrieve OData feed
            List<ODataItem> odataItems;
            try
            {
                odataItems = await FetchODataAsync(odataUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to fetch OData: {ex.Message}");
                return;
            }

            // Build a lookup dictionary for fast access (keyed by Id)
            var lookup = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in odataItems)
            {
                if (!string.IsNullOrEmpty(item.Id))
                {
                    lookup[item.Id] = item.Value ?? string.Empty;
                }
            }

            // Iterate through all pages and shapes, updating Data1 where shape name matches OData Id
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Use the universal name (NameU) for matching
                    string shapeKey = shape.NameU;
                    if (string.IsNullOrEmpty(shapeKey))
                        continue;

                    if (lookup.TryGetValue(shapeKey, out string newValue))
                    {
                        // Update the custom data field
                        shape.Data1 = newValue;
                        Console.WriteLine($"Updated shape '{shapeKey}' Data1 to '{newValue}'.");
                    }
                }
            }

            // Save the updated diagram
            string outputPath = System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(visioPath) ?? string.Empty,
                System.IO.Path.GetFileNameWithoutExtension(visioPath) + "_Updated.vsdx");

            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }

        // Helper method to fetch and deserialize OData JSON array
        private static async Task<List<ODataItem>> FetchODataAsync(string requestUrl)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            // Assuming the OData service returns a JSON array of objects with 'Id' and 'Value' properties
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<ODataItem>>(json, options) 
                   ?? new List<ODataItem>();
        }
    }
}