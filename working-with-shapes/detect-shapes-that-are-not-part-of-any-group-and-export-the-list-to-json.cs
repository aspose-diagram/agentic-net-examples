using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioFile> [outputJsonFile]");
            return;
        }

        // Input Visio file path.
        string inputPath = args[0];
        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output JSON file path (default if not supplied).
        string outputPath = args.Length > 1 ? args[1] : "nonGroupedShapes.json";

        // Guard: ensure the output directory exists.
        string? outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Load the Visio diagram inside a try/catch to capture loading errors.
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

        // List to hold information about shapes that are not part of any group.
        List<ShapeInfo> nonGroupedShapes = new List<ShapeInfo>();

        // Iterate over each page in the diagram.
        for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
        {
            Page page = diagram.Pages[pageIndex];

            // Iterate over each shape on the current page.
            foreach (Shape shape in page.Shapes)
            {
                // Skip shapes that belong to a group.
                if (shape.IsInGroup())
                {
                    continue;
                }

                // Gather basic shape details.
                ShapeInfo info = new ShapeInfo
                {
                    PageIndex = pageIndex,
                    ShapeId = shape.ID,
                    NameU = shape.NameU,
                    MasterName = shape.Master?.Name
                };

                nonGroupedShapes.Add(info);
            }
        }

        // Serialize the collected shape information to JSON.
        string json;
        try
        {
            json = JsonSerializer.Serialize(nonGroupedShapes, new JsonSerializerOptions { WriteIndented = true });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during JSON serialization: {ex.Message}");
            return;
        }

        // Write the JSON output to the specified file.
        try
        {
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Exported {nonGroupedShapes.Count} non‑grouped shapes to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing JSON file: {ex.Message}");
        }
    }
}

// DTO representing the minimal information for each non‑grouped shape.
class ShapeInfo
{
    public int PageIndex { get; set; }          // Zero‑based index of the page containing the shape.
    public long ShapeId { get; set; }           // Unique identifier of the shape.
    public string? NameU { get; set; }          // Universal name of the shape (may be null).
    public string? MasterName { get; set; }     // Name of the master shape, if any (may be null).
}