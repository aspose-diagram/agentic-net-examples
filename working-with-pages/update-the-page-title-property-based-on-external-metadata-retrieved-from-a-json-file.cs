using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramMetadataUpdater
{
    // Simple class to map JSON metadata; Title may be missing, so make it nullable.
    public class Metadata
    {
        public string? Title { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Paths can be hard‑coded or passed via command‑line arguments
            string diagramPath = "input.vsdx";
            string jsonPath = "metadata.json";
            string outputPath = "output.vsdx";

            // Guard: ensure the diagram file exists before proceeding
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                return;
            }

            // Guard: ensure the JSON metadata file exists before proceeding
            if (!File.Exists(jsonPath))
            {
                Console.Error.WriteLine($"File not found: {jsonPath}");
                return;
            }

            // Load external metadata from JSON file
            string jsonContent = File.ReadAllText(jsonPath);
            Metadata? metadata = JsonSerializer.Deserialize<Metadata>(jsonContent);
            if (metadata == null || string.IsNullOrWhiteSpace(metadata.Title))
            {
                Console.Error.WriteLine("Invalid or missing Title in metadata JSON.");
                return;
            }

            try
            {
                // Load the Visio diagram
                using Diagram diagram = new Diagram(diagramPath);
                // Update the built‑in Title property with the value from JSON
                diagram.DocumentProps.Title = metadata.Title;
                // Save the updated diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Write any Aspose‑related errors to the error stream
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
                return;
            }

            // Inform the user that the operation succeeded
            Console.WriteLine($"Diagram saved with updated title: \"{metadata.Title}\"");
        }
    }
}