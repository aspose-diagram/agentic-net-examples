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

                // Path to the Visio diagram to be processed
                const string diagramPath = "input.vsdx";

                // Load the diagram using the Aspose.Diagram constructor
                Diagram diagram = new Diagram(diagramPath);

                // REST endpoint that returns JSON data
                const string apiUrl = "https://example.com/api/data";

                // Retrieve JSON payload from the REST endpoint
                using HttpClient httpClient = new HttpClient();
                string jsonResponse = await httpClient.GetStringAsync(apiUrl);

                // Parse the JSON document
                using JsonDocument jsonDoc = JsonDocument.Parse(jsonResponse);
                JsonElement root = jsonDoc.RootElement;

                // Iterate through all pages and shapes in the diagram
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Use the shape's universal name (NameU) as the key to look up data in the JSON
                        if (root.TryGetProperty(shape.NameU, out JsonElement valueElement))
                        {
                            // Look for an existing user-defined cell named "CustomData"
                            User? customUser = null;
                            foreach (User existingUser in shape.Users)
                            {
                                if (existingUser.Name == "CustomData")
                                {
                                    customUser = existingUser;
                                    break;
                                }
                            }

                            // If the user-defined cell does not exist, create and add it
                            if (customUser == null)
                            {
                                customUser = new User();
                                customUser.Name = "CustomData";
                                shape.Users.Add(customUser);
                            }

                            // Assign the JSON value (as string) to the user-defined cell
                            // For non-string JSON types, fallback to the raw JSON text
                            string valueAsString = valueElement.ValueKind == JsonValueKind.String
                                ? valueElement.GetString()!
                                : valueElement.GetRawText();

                            customUser.Value.Val = valueAsString;
                        }
                    }
                }

                // Save the updated diagram to a new file
                const string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }