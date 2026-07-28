using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Aspose.Diagram;

namespace ODataDiagramSync
{
    // Represents a single record from the OData feed.
    public class ODataItem
    {
        public string ShapeName { get; set; }
        public string Data1 { get; set; }
        public string Data2 { get; set; }
        public string Data3 { get; set; }
    }

    public class Program
    {
        // Entry point of the console application.
        public static async Task Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: ODataDiagramSync <inputVisioPath> <outputVisioPath> <odataUrl>");
                return;
            }

            string inputVisioPath = args[0];
            string outputVisioPath = args[1];
            string odataUrl = args[2];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputVisioPath);

            // Retrieve OData feed.
            List<ODataItem> odataItems = await FetchODataAsync(odataUrl);
            if (odataItems == null)
            {
                Console.WriteLine("Failed to retrieve OData feed.");
                return;
            }

            // Build a lookup dictionary for fast shape matching.
            Dictionary<string, ODataItem> lookup = new Dictionary<string, ODataItem>(StringComparer.OrdinalIgnoreCase);
            foreach (ODataItem item in odataItems)
            {
                if (!string.IsNullOrWhiteSpace(item.ShapeName))
                {
                    lookup[item.ShapeName] = item;
                }
            }

            // Iterate through all pages and shapes, updating Data fields where a match is found.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU != null && lookup.TryGetValue(shape.NameU, out ODataItem match))
                    {
                        // Update shape data fields directly (no .Value needed for Data1/Data2/Data3).
                        shape.Data1 = match.Data1 ?? string.Empty;
                        shape.Data2 = match.Data2 ?? string.Empty;
                        shape.Data3 = match.Data3 ?? string.Empty;

                        Console.WriteLine($"Updated shape '{shape.NameU}' (ID: {shape.ID}) with OData values.");
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputVisioPath}'.");
        }

        // Helper method to fetch and deserialize OData JSON.
        private static async Task<List<ODataItem>> FetchODataAsync(string url)
        {
            try
            {
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                List<ODataItem> items = JsonSerializer.Deserialize<List<ODataItem>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching OData: {ex.Message}");
                return null;
            }
        }
    }
}