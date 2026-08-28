using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Paths to the JSON metadata file and the Visio diagram.
        string jsonPath = "metadata.json";
        string diagramPath = "input.vsdx";
        string outputPath = "output.vsdx";

        // Verify JSON file exists.
        if (!File.Exists(jsonPath))
        {
            Console.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Read and deserialize the JSON metadata.
        string jsonContent = File.ReadAllText(jsonPath);
        Metadata? meta = JsonSerializer.Deserialize<Metadata>(jsonContent);
        if (meta == null || string.IsNullOrWhiteSpace(meta.Title))
        {
            Console.WriteLine("Invalid metadata or missing Title property.");
            return;
        }

        // Verify diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Load the diagram, update the title, and save.
        using (Diagram diagram = new Diagram(diagramPath))
        {
            diagram.DocumentProps.Title = meta.Title;
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }

        Console.WriteLine($"Diagram saved with updated title to {outputPath}");
    }

    // Simple class matching the JSON structure.
    private class Metadata
    {
        public string Title { get; set; } = string.Empty;
    }
}