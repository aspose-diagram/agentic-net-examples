using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine JSON metadata file path (ignore arguments that look like options)
        string jsonPath = (args.Length > 0 && !args[0].StartsWith("-")) ? args[0] : "metadata.json";
        // Guard: ensure the JSON file exists before proceeding
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"File not found: {jsonPath}");
            return;
        }

        // Determine Visio diagram input file path (ignore option-like arguments)
        string diagramPath = (args.Length > 1 && !args[1].StartsWith("-")) ? args[1] : "input.vsdx";
        // Guard: ensure the diagram file exists before loading
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Determine output file path (fallback to default if not provided)
        string outputPath = (args.Length > 2 && !args[2].StartsWith("-")) ? args[2] : "output.vsdx";

        // Read JSON content from the metadata file
        string jsonContent;
        try
        {
            jsonContent = File.ReadAllText(jsonPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read JSON file '{jsonPath}': {ex.Message}");
            return;
        }

        // Parse JSON and extract the "title" property
        string title;
        try
        {
            using JsonDocument doc = JsonDocument.Parse(jsonContent);
            if (doc.RootElement.TryGetProperty("title", out JsonElement titleElement) &&
                titleElement.ValueKind == JsonValueKind.String)
            {
                title = titleElement.GetString() ?? string.Empty;
            }
            else
            {
                Console.Error.WriteLine("JSON does not contain a string property named 'title'.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse JSON metadata: {ex.Message}");
            return;
        }

        // Load the Visio diagram, update its title property, and save the result
        try
        {
            using Diagram diagram = new Diagram(diagramPath);
            diagram.DocumentProps.Title = title;               // Update built‑in title property
            diagram.Save(outputPath, SaveFileFormat.Vsdx);     // Save using the required overload
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram '{diagramPath}': {ex.Message}");
            return;
        }

        // Inform the user of successful completion
        Console.WriteLine($"Diagram title updated to '{title}' and saved to '{outputPath}'.");
    }
}