using System.IO;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static async Task Main()
    {
        try
        {

            // Load an existing Visio diagram
            string diagramPath = "input.vsdx";
            Diagram diagram = new Diagram(diagramPath);

            // REST endpoint that returns JSON data
            string url = "https://example.com/api/data";

            // Retrieve JSON from the endpoint
            using HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(url);

            // Deserialize JSON into a dictionary of string key/value pairs
            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            if (data == null)
            {
                Console.WriteLine("Failed to parse JSON data.");
                return;
            }

            // Iterate through all pages and shapes to assign custom properties
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // For each key/value pair from the JSON, create or update a custom property on the shape
                    foreach (KeyValuePair<string, string> kvp in data)
                    {
                        // Check if the property already exists on the shape
                        Prop existingProp = null;
                        foreach (Prop p in shape.Props)
                        {
                            if (p.Name == kvp.Key)
                            {
                                existingProp = p;
                                break;
                            }
                        }

                        if (existingProp != null)
                        {
                            // Update existing property value
                            existingProp.Value.Val = kvp.Value;
                        }
                        else
                        {
                            // Create a new custom property
                            Prop newProp = new Prop();
                            newProp.Name = kvp.Key;                     // Property name
                            newProp.Label.Value = kvp.Key;              // Display label
                            newProp.Type.Value = TypePropValue.String;  // Data type
                            newProp.Value.Val = kvp.Value;              // Property value
                            shape.Props.Add(newProp);
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
