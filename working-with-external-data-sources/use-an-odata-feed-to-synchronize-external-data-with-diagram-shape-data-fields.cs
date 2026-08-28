using System;
using System.Net.Http;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file to be updated
            string diagramPath = "input.vsdx";

            // OData service endpoint returning JSON data (adjust URL as needed)
            string odataUrl = "https://example.com/odata/Items";

            // Load the existing diagram
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve OData JSON payload
            using (HttpClient httpClient = new HttpClient())
            {
                string json = httpClient.GetStringAsync(odataUrl).Result;

                // Deserialize JSON into an array of items (Id and Value fields expected)
                var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Item[] items = JsonSerializer.Deserialize<Item[]>(json, jsonOptions);

                // Build a lookup dictionary keyed by Id for fast access
                var lookup = new System.Collections.Generic.Dictionary<string, string>();
                if (items != null)
                {
                    foreach (Item item in items)
                    {
                        if (!string.IsNullOrEmpty(item.Id))
                        {
                            lookup[item.Id] = item.Value ?? string.Empty;
                        }
                    }
                }

                // Iterate through all pages and shapes in the diagram
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Use the universal shape name (NameU) as the key to match OData records
                        string key = shape.NameU;
                        if (lookup.TryGetValue(key, out string value))
                        {
                            // Synchronize external data into shape's custom data fields
                            shape.Data1 = value;                                 // Store external value
                            shape.Data2 = DateTime.Now.ToString("yyyy-MM-dd");   // Example: store sync timestamp
                        }
                    }
                }
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper class representing the expected OData JSON structure
    private class Item
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }
}