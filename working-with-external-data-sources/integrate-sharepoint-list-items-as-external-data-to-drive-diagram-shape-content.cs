using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // SharePoint REST endpoint configuration (replace placeholders with real values)
        string sharepointSite = "https://yourtenant.sharepoint.com/sites/YourSite";
        string listName = "YourList";
        string accessToken = "YOUR_ACCESS_TOKEN";

        // Build the request URL for list items
        string requestUrl = $"{sharepointSite}/_api/web/lists/getbytitle('{listName}')/items";

        // Retrieve list items from SharePoint with error handling
        JsonElement results;
        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");

            HttpResponseMessage response = client.GetAsync(requestUrl).Result;
            response.EnsureSuccessStatusCode();
            string json = response.Content.ReadAsStringAsync().Result;

            // Parse the JSON response
            using JsonDocument doc = JsonDocument.Parse(json);
            results = doc.RootElement.GetProperty("d").GetProperty("results");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error retrieving SharePoint data: {ex.Message}");
            return;
        }

        // Load a Visio template (ensure the file exists and contains the required masters)
        string templatePath = "template.vsdx";
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"File not found: {templatePath}");
            return;
        }

        Diagram diagram;
        try
        {
            diagram = new Diagram(templatePath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Use the first page of the diagram
        Page page = diagram.Pages[0];

        // Positioning variables for added shapes
        double startX = 1.0;          // inches from left edge
        double startY = 1.0;          // inches from top edge
        double verticalSpacing = 1.5; // inches between shapes

        int shapeIndex = 0;

        // Iterate over each SharePoint list item and create a shape
        foreach (JsonElement item in results.EnumerateArray())
        {
            // Extract fields from the SharePoint item (adjust field names as needed)
            string title = item.GetProperty("Title").GetString() ?? string.Empty;
            string description = item.GetProperty("Description").GetString() ?? string.Empty;

            // Calculate shape position
            double pinX = startX;
            double pinY = startY + verticalSpacing * shapeIndex;

            try
            {
                // Add a rectangle shape using the master name "Rectangle" on page index 0
                long shapeIdLong = diagram.AddShape(pinX, pinY, "Rectangle", 0);
                Shape shape = page.Shapes.GetShape((int)shapeIdLong);

                // Set the shape's text to display the SharePoint item data
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt($"{title}\n{description}"));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating shape for item '{title}': {ex.Message}");
                // Continue processing remaining items
            }

            shapeIndex++;
        }

        // Save the updated diagram to a new file
        string outputPath = "output.vsdx";
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}