using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Simple POCO to hold OData items
    private class ODataItem
    {
        public long Id { get; set; }
        public string FieldValue { get; set; }
    }

    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <outputPath> <odataUrl>");
            return;
        }

        string diagramPath = args[0];
        string outputPath = args[1];
        string odataUrl = args[2];

        // Guard against missing diagram file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Load the diagram inside a try/catch block
        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Fetch OData JSON synchronously
        string json;
        try
        {
            using (HttpClient client = new HttpClient())
            {
                json = client.GetStringAsync(odataUrl).Result;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching OData from '{odataUrl}': {ex.Message}");
            return;
        }

        // Deserialize OData into a list of items
        List<ODataItem> odataItems;
        try
        {
            odataItems = JsonSerializer.Deserialize<List<ODataItem>>(json);
            if (odataItems == null)
            {
                Console.Error.WriteLine("OData response deserialized to null.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error parsing OData JSON: {ex.Message}");
            return;
        }

        // Create a lookup dictionary for fast ID matching
        Dictionary<long, string> dataMap = new Dictionary<long, string>();
        foreach (ODataItem item in odataItems)
        {
            if (!dataMap.ContainsKey(item.Id))
            {
                dataMap.Add(item.Id, item.FieldValue ?? string.Empty);
            }
        }

        // Iterate through all pages and shapes to synchronize data
        try
        {
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Check if the shape ID exists in the OData payload
                    if (dataMap.TryGetValue(shape.ID, out string newValue))
                    {
                        // Look for an existing user-defined cell named "ODataValue"
                        Aspose.Diagram.User existingUser = null;
                        foreach (Aspose.Diagram.User user in shape.Users)
                        {
                            if (string.Equals(user.NameU, "ODataValue", StringComparison.OrdinalIgnoreCase))
                            {
                                existingUser = user;
                                break;
                            }
                        }

                        if (existingUser != null)
                        {
                            // Update the existing user-defined cell
                            existingUser.Value.Val = newValue;
                        }
                        else
                        {
                            // Create a new user-defined cell and add it to the shape
                            Aspose.Diagram.User newUser = new Aspose.Diagram.User();
                            newUser.NameU = "ODataValue";
                            newUser.Value.Val = newValue;
                            shape.Users.Add(newUser);
                        }
                    }
                }
            }

            // Refresh the diagram to apply any data connection changes
            diagram.Refresh();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during synchronization: {ex.Message}");
            return;
        }

        // Save the updated diagram
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}