using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        // Entry point of the console application
        static async Task Main(string[] args)
        {
            try
            {

                // Path to the source Visio diagram
                const string inputPath = "input.vsdx";
                // Path where the updated diagram will be saved
                const string outputPath = "output.vsdx";
                // URL of the external data feed that provides shape name to URL mappings (JSON format)
                const string dataFeedUrl = "https://example.com/api/shape-links";

                // Load the diagram using the Aspose.Diagram constructor
                Diagram diagram = new Diagram(inputPath);

                // Retrieve the mapping of shape names to hyperlink URLs
                Dictionary<string, string> shapeUrlMap = await GetShapeUrlMappingAsync(dataFeedUrl);

                // Iterate through all pages and shapes in the diagram
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Use the universal name (NameU) as the key for lookup
                        string shapeKey = shape.NameU;

                        if (shapeUrlMap != null && shapeUrlMap.ContainsKey(shapeKey))
                        {
                            // Ensure the Hyperlinks collection is not null before adding
                            if (shape.Hyperlinks != null)
                            {
                                // Create a new hyperlink instance
                                Hyperlink link = new Hyperlink();
                                link.Name = "ExternalLink";
                                link.Address.Value = shapeUrlMap[shapeKey];

                                // Add the hyperlink to the shape's collection
                                shape.Hyperlinks.Add(link);
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Retrieves a dictionary mapping shape names to URLs from a JSON endpoint
        private static async Task<Dictionary<string, string>> GetShapeUrlMappingAsync(string requestUrl)
        {
            using HttpClient client = new HttpClient();

            try
            {
                // Perform the HTTP GET request
                HttpResponseMessage response = await client.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string json = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON into a dictionary (expects {"ShapeName":"https://..."} format)
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<Dictionary<string, string>>(json, options);
            }
            catch (Exception ex)
            {
                // In case of any errors, write to console and return an empty dictionary
                Console.WriteLine($"Error retrieving shape URL mapping: {ex.Message}");
                return new Dictionary<string, string>();
            }
        }
    }