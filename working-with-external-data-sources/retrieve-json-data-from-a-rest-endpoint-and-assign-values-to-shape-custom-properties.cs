using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
    {
        // Entry point of the console application
        static async Task Main(string[] args)
        {
            try
            {

                // Paths for the source Visio diagram and the output file
                string diagramPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // REST endpoint that returns JSON data
                string jsonEndpoint = "https://example.com/api/shapes";

                // Load the diagram from file
                Diagram diagram = new Diagram(diagramPath);

                // Retrieve JSON data from the REST endpoint
                using HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(jsonEndpoint);
                response.EnsureSuccessStatusCode();
                string jsonContent = await response.Content.ReadAsStringAsync();

                // Expected JSON format:
                // [
                //   { "ShapeId": 5, "PropertyName": "Status", "PropertyValue": "Approved" },
                //   { "ShapeId": 12, "PropertyName": "Owner", "PropertyValue": "John Doe" }
                // ]
                JsonDocument jsonDoc = JsonDocument.Parse(jsonContent);
                foreach (JsonElement element in jsonDoc.RootElement.EnumerateArray())
                {
                    // Extract values from JSON
                    long shapeId = element.GetProperty("ShapeId").GetInt64();
                    string propName = element.GetProperty("PropertyName").GetString();
                    string propValue = element.GetProperty("PropertyValue").GetString();

                    // Locate the shape by ID across all pages
                    Shape targetShape = null;
                    foreach (Page page in diagram.Pages)
                    {
                        // Shape IDs are long; GetShape expects an int, so cast safely
                        if (page.Shapes.GetShape((int)shapeId) != null)
                        {
                            targetShape = page.Shapes.GetShape((int)shapeId);
                            break;
                        }
                    }

                    if (targetShape == null)
                    {
                        Console.WriteLine($"Shape with ID {shapeId} not found.");
                        continue;
                    }

                    // Create a new custom property (Prop) and assign values
                    Prop customProp = new Prop
                    {
                        Name = propName,
                        // Optional: set a label; using the same name if not required
                        Label = { Value = propName },
                        // Assign the actual value
                        Value = { Val = propValue },
                        // Define the property type as string
                        Type = { Value = TypePropValue.String }
                    };

                    // Add the custom property to the shape
                    targetShape.Props.Add(customProp);
                    Console.WriteLine($"Added property '{propName}' with value '{propValue}' to shape ID {shapeId}.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }