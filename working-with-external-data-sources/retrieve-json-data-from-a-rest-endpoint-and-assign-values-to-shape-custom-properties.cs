using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // REST endpoint returning JSON in the form {"ShapeNameU":"PropertyValue", ...}
        string endpoint = "https://example.com/api/data";

        // Retrieve JSON data with error handling
        string json;
        try
        {
            using var httpClient = new HttpClient();
            // Synchronously wait for the async call to avoid async Main (classic style)
            json = httpClient.GetStringAsync(endpoint).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            // Log HTTP errors and abort execution
            Console.Error.WriteLine($"Error retrieving JSON from endpoint: {ex.Message}");
            return;
        }

        // Deserialize to a dictionary for easy lookup
        var data = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        if (data == null)
        {
            Console.Error.WriteLine("Failed to deserialize JSON data.");
            return;
        }

        // Path to the input Visio diagram
        string inputPath = "input.vsdx";
        // Guard to ensure the file exists before loading
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the diagram inside a try/catch to capture Aspose errors
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Iterate through all pages and shapes, updating/adding custom properties
        try
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Use the universal shape name (NameU) as the key to find matching data
                    if (data.TryGetValue(shape.NameU, out string propValue))
                    {
                        // Search for an existing custom property named "CustomData"
                        Prop existingProp = null;
                        foreach (Prop p in shape.Props)
                        {
                            if (p.Name == "CustomData")
                            {
                                existingProp = p;
                                break;
                            }
                        }

                        if (existingProp == null)
                        {
                            // Create a new custom property and add it to the shape
                            var newProp = new Prop();
                            newProp.Name = "CustomData";
                            newProp.Label.Value = "Custom Data";
                            newProp.Value.Val = propValue;
                            shape.Props.Add(newProp);
                            Console.WriteLine($"Added CustomData to shape '{shape.NameU}' with value '{propValue}'.");
                        }
                        else
                        {
                            // Update the existing property's value
                            existingProp.Value.Val = propValue;
                            Console.WriteLine($"Updated CustomData on shape '{shape.NameU}' to '{propValue}'.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram shapes: {ex.Message}");
            return;
        }

        // Save the modified diagram with error handling
        try
        {
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved as output.vsdx");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}