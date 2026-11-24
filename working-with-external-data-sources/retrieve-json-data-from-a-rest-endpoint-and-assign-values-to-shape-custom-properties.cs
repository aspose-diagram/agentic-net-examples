using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: inputVisioPath, outputVisioPath, jsonEndpointUrl
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <jsonEndpointUrl>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string jsonUrl = args[2];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Retrieve JSON data from the REST endpoint
        Dictionary<long, Dictionary<string, string>> shapeData = FetchJsonData(jsonUrl);
        if (shapeData == null)
        {
            // Error already reported inside FetchJsonData
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if we have data for this shape ID
                        if (shapeData.TryGetValue(shape.ID, out var properties))
                        {
                            // Assign each property to the shape's custom properties
                            foreach (var kvp in properties)
                            {
                                string propName = kvp.Key;
                                string propValue = kvp.Value;

                                // Try to find an existing custom property with the same name
                                Prop existingProp = null;
                                foreach (Prop p in shape.Props)
                                {
                                    if (p.Name == propName)
                                    {
                                        existingProp = p;
                                        break;
                                    }
                                }

                                if (existingProp != null)
                                {
                                    // Update the value of the existing property
                                    existingProp.Value.Val = propValue;
                                }
                                else
                                {
                                    // Create a new custom property and add it to the shape
                                    Prop newProp = new Prop();
                                    newProp.Name = propName;
                                    newProp.Label.Value = propName;               // Use the property name as label
                                    newProp.Value.Val = propValue;                // Assign the actual value
                                    newProp.Type.Value = TypePropValue.String;    // Set data type to string
                                    shape.Props.Add(newProp);
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves JSON from the specified URL and parses it into a dictionary.
    /// Expected JSON format:
    /// [
    ///   {
    ///     "shapeId": 1,
    ///     "properties": {
    ///       "PropA": "ValueA",
    ///       "PropB": "ValueB"
    ///     }
    ///   },
    ///   ...
    /// ]
    /// </summary>
    static Dictionary<long, Dictionary<string, string>> FetchJsonData(string url)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                // Synchronously get the JSON string (for simplicity in a console app)
                HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
                string json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                // Parse the JSON document
                var result = new Dictionary<long, Dictionary<string, string>>();
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    if (doc.RootElement.ValueKind != JsonValueKind.Array)
                    {
                        Console.Error.WriteLine("Unexpected JSON format: root element is not an array.");
                        return null;
                    }

                    foreach (JsonElement element in doc.RootElement.EnumerateArray())
                    {
                        if (!element.TryGetProperty("shapeId", out JsonElement idElem) ||
                            !element.TryGetProperty("properties", out JsonElement propsElem))
                        {
                            continue; // Skip malformed entries
                        }

                        long shapeId = idElem.GetInt64();
                        var propDict = new Dictionary<string, string>();

                        foreach (JsonProperty prop in propsElem.EnumerateObject())
                        {
                            propDict[prop.Name] = prop.Value.GetString();
                        }

                        result[shapeId] = propDict;
                    }
                }

                return result;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to retrieve or parse JSON data: {ex.Message}");
            return null;
        }
    }
}