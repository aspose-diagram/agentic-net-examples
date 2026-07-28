using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
    {
        // Entry point
        static async Task Main(string[] args)
        {
            // Validate arguments
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: SharePointDiagramIntegration <VisioFilePath> <SharePointSiteUrl> <ListName>");
                return;
            }

            string visioPath = args[0];
            string siteUrl = args[1];
            string listName = args[2];

            // Load Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(visioPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load Visio file: {ex.Message}");
                return;
            }

            // Retrieve SharePoint list items
            List<Dictionary<string, string>> listItems;
            try
            {
                listItems = await GetSharePointListItemsAsync(siteUrl, listName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve SharePoint list items: {ex.Message}");
                return;
            }

            // Map list items to shapes (example: match by Title to shape NameU)
            foreach (var page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Find a list item whose "Title" matches the shape's universal name
                    foreach (var item in listItems)
                    {
                        if (item.TryGetValue("Title", out string title) && shape.NameU == title)
                        {
                            // Update shape text with the "Title" and optionally other fields
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt(title));

                            // Example: append a description field if present
                            if (item.TryGetValue("Description", out string description) && !string.IsNullOrWhiteSpace(description))
                            {
                                shape.Text.Value.Add(new Txt("\n" + description));
                            }

                            // Once matched, break inner loop
                            break;
                        }
                    }
                }
            }

            // Save the updated diagram
            string outputPath = System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(visioPath) ?? "",
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

        // Retrieves items from a SharePoint list using the REST API.
        // Returns a list of dictionaries where each dictionary represents a list item (field name -> value).
        private static async Task<List<Dictionary<string, string>>> GetSharePointListItemsAsync(string siteUrl, string listName)
        {
            var items = new List<Dictionary<string, string>>();

            // Construct the REST endpoint URL
            string requestUrl = $"{siteUrl.TrimEnd('/')}/_api/web/lists/getbytitle('{listName}')/items";

            using var httpClient = new HttpClient();

            // Set headers to accept JSON
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // If authentication is required, configure HttpClient accordingly.
            // For example, use DefaultCredentials for on-premises NTLM authentication:
            // httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "<access_token>");

            using var response = await httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            using var contentStream = await response.Content.ReadAsStreamAsync();

            // Parse the JSON response
            using var jsonDoc = await JsonDocument.ParseAsync(contentStream);
            if (jsonDoc.RootElement.TryGetProperty("value", out JsonElement valueArray) && valueArray.ValueKind == JsonValueKind.Array)
            {
                foreach (var element in valueArray.EnumerateArray())
                {
                    var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    foreach (var property in element.EnumerateObject())
                    {
                        // Store each field as string (null values become empty strings)
                        dict[property.Name] = property.Value.GetString() ?? string.Empty;
                    }
                    items.Add(dict);
                }
            }

            return items;
        }
    }