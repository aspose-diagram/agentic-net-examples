using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (default to "input.vsdx" if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Output JSON file path (default to "metadata.json" if not provided)
        string outputPath = args.Length > 1 ? args[1] : "metadata.json";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare a dictionary to hold metadata
            var metadata = new Dictionary<string, object>();

            // Built‑in document properties
            // Title
            metadata["Title"] = diagram.DocumentProps.Title ?? string.Empty;

            // Author (use Creator if Author is not available)
            metadata["Author"] = diagram.DocumentProps.Creator ?? string.Empty;

            // Creation date (ISO 8601 format)
            DateTime created = diagram.DocumentProps.TimeCreated;
            metadata["Created"] = created.ToString("o");

            // Last edited date (ISO 8601 format)
            DateTime edited = diagram.DocumentProps.TimeEdited;
            metadata["LastEdited"] = edited.ToString("o");

            // Build numbers (read‑only system metadata)
            metadata["BuildNumberCreated"] = diagram.DocumentProps.BuildNumberCreated;
            metadata["BuildNumberEdited"] = diagram.DocumentProps.BuildNumberEdited;

            // Version of the Visio instance that created the file
            metadata["Version"] = diagram.Version ?? string.Empty;

            // Serialize dictionary to pretty‑printed JSON
            string json = JsonSerializer.Serialize(metadata, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the output file
            File.WriteAllText(outputPath, json);

            Console.WriteLine($"Metadata extracted to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}