using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    // Model representing JSON data for a shape's custom property
    private class ShapeData
    {
        public int ShapeId { get; set; }
        public string PropName { get; set; }
        public string PropValue { get; set; }
    }

    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <diagramPath> <jsonUrl> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string jsonUrl = args[1];
        string outputPath = args[2];

        // Guard against missing diagram file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Retrieve JSON data from the REST endpoint
        List<ShapeData> shapeDataList = GetShapeDataFromJson(jsonUrl);
        if (shapeDataList == null)
        {
            Console.Error.WriteLine("Failed to retrieve or parse JSON data.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Find matching JSON entry for the current shape ID
                    ShapeData matchingData = shapeDataList.Find(d => d.ShapeId == shape.ID);
                    if (matchingData == null)
                        continue; // No data for this shape, skip

                    // Check if the custom property already exists
                    Prop existingProp = null;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == matchingData.PropName)
                        {
                            existingProp = prop;
                            break;
                        }
                    }

                    if (existingProp != null)
                    {
                        // Update existing property's value
                        existingProp.Value.Val = matchingData.PropValue;
                    }
                    else
                    {
                        // Create a new custom property and add it to the shape
                        Prop newProp = new Prop();
                        newProp.Name = matchingData.PropName;
                        newProp.Label.Value = matchingData.PropName;          // Label displayed in the Shapesheet
                        newProp.Value.Val = matchingData.PropValue;          // Actual value
                        newProp.Type.Value = TypePropValue.String;          // Define as string type
                        shape.Props.Add(newProp);
                    }
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Retrieves and deserializes JSON data from the given URL
    private static List<ShapeData> GetShapeDataFromJson(string url)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                // Synchronous call to keep Main non‑async
                var response = client.GetAsync(url).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();

                var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var data = JsonSerializer.Deserialize<List<ShapeData>>(json);
                return data ?? new List<ShapeData>();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to fetch JSON from '{url}': {ex.Message}");
            return null;
        }
    }
}