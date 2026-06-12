using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output JSON file path
        string outputPath = "metadata.json";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Extract built‑in document properties
            string? author = diagram.DocumentProps.Creator;
            string? title = diagram.DocumentProps.Title;
            DateTime created = diagram.DocumentProps.TimeCreated;
            DateTime edited = diagram.DocumentProps.TimeEdited;

            // Prepare an anonymous object for JSON serialization
            var metadata = new
            {
                Author = author,
                Title = title,
                Created = created,
                Edited = edited
            };

            // Serialize to JSON with indentation
            string json = JsonSerializer.Serialize(metadata, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to file
            File.WriteAllText(outputPath, json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        Console.WriteLine("Metadata extraction completed.");
    }
}