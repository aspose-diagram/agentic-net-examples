using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input arguments: diagram file, JSON metadata file, optional output file
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
        string jsonPath = args.Length > 1 ? args[1] : "metadata.json";
        string outputPath = args.Length > 2 ? args[2] : "output.vsdx";

        // Guard: ensure diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure JSON metadata file exists
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"Metadata JSON file not found: {jsonPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read and parse the JSON metadata
            string jsonContent = File.ReadAllText(jsonPath);
            using JsonDocument doc = JsonDocument.Parse(jsonContent);
            JsonElement root = doc.RootElement;

            // Expecting an array of objects with "PageIndex" and "Title"
            if (root.ValueKind != JsonValueKind.Array)
            {
                Console.Error.WriteLine("Invalid JSON format: expected an array of page metadata.");
                return;
            }

            foreach (JsonElement element in root.EnumerateArray())
            {
                // Extract page index
                if (!element.TryGetProperty("PageIndex", out JsonElement pageIdxElem) ||
                    !pageIdxElem.TryGetInt32(out int pageIndex))
                {
                    Console.Error.WriteLine("Skipping entry: missing or invalid PageIndex.");
                    continue;
                }

                // Extract title
                if (!element.TryGetProperty("Title", out JsonElement titleElem) ||
                    titleElem.GetString() is not string title)
                {
                    Console.Error.WriteLine($"Skipping entry for page {pageIndex}: missing Title.");
                    continue;
                }

                // Validate page index range
                if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Page index out of range: {pageIndex}");
                    continue;
                }

                // Update the page title (using NameU as the title property)
                Page page = diagram.Pages[pageIndex];
                page.NameU = title;
                // Also update the visible name if needed
                page.Name = title;
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}