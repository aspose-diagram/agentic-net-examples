using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // DTO matching the expected JSON structure.
    private class Metadata
    {
        public string? PageTitle { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, metadata JSON file, output Visio file.
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <metadataJsonPath> <outputVisioPath>");
            return;
        }

        string visioPath = args[0];
        string jsonPath = args[1];
        string outputPath = args[2];

        // Guard against missing files.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON metadata file not found: {jsonPath}");
            return;
        }

        // Load metadata from JSON.
        Metadata? metadata;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            metadata = JsonSerializer.Deserialize<Metadata>(jsonContent);
            if (metadata == null || string.IsNullOrWhiteSpace(metadata.PageTitle))
            {
                Console.Error.WriteLine("Invalid or empty metadata in JSON file.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading JSON file: {ex.Message}");
            return;
        }

        // Load the Visio diagram, update page titles, and save.
        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate through all pages and set the Name (title) property.
                foreach (Page page in diagram.Pages)
                {
                    // Update the page name and universal name with the metadata title.
                    page.Name = metadata.PageTitle;
                    page.NameU = metadata.PageTitle;
                }

                // Save the updated diagram to the output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Aspose.Diagram operation failed: {ex.Message}");
            return;
        }

        Console.WriteLine("Page titles updated and diagram saved successfully.");
    }
}