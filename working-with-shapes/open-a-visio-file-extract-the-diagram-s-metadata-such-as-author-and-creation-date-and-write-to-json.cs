using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access built‑in document properties.
                DocumentProperties props = diagram.DocumentProps;

                // Build an object containing the desired metadata.
                var metadata = new
                {
                    Author = props.Creator,
                    Company = props.Company,
                    Title = props.Title,
                    Subject = props.Subject,
                    Category = props.Category,
                    Manager = props.Manager,
                    Created = props.TimeCreated.ToString("o"),
                    Saved = props.TimeSaved.ToString("o"),
                    Version = diagram.Version,
                    BuildNumberCreated = props.BuildNumberCreated,
                    BuildNumberEdited = props.BuildNumberEdited
                };

                // Serialize the metadata object to formatted JSON.
                string json = JsonSerializer.Serialize(metadata, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the specified output file.
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Metadata successfully written to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}