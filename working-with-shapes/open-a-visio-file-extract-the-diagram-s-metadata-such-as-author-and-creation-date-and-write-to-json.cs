using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "metadata.json";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Extract built‑in document properties
                string author = diagram.DocumentProps.Creator;
                string title = diagram.DocumentProps.Title;
                string subject = diagram.DocumentProps.Subject;
                string company = diagram.DocumentProps.Company;
                string manager = diagram.DocumentProps.Manager;
                string category = diagram.DocumentProps.Category;

                // Extract time‑related properties (DateTime values)
                DateTime createdDate = diagram.DocumentProps.TimeCreated;
                DateTime lastSavedDate = diagram.DocumentProps.TimeSaved;

                // Prepare an anonymous object for JSON serialization
                var metadata = new
                {
                    Author = author,
                    Title = title,
                    Subject = subject,
                    Company = company,
                    Manager = manager,
                    Category = category,
                    CreatedDate = createdDate,
                    LastSavedDate = lastSavedDate
                };

                // Serialize metadata to JSON with indentation
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(metadata, jsonOptions);

                // Write JSON to the output file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Metadata extracted and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}