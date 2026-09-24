using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string visioPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Extract required metadata
                var metadata = new
                {
                    Author = diagram.DocumentProps.Creator,
                    CreatedDate = diagram.DocumentProps.TimeCreated
                };

                // Serialize metadata to JSON with indentation
                string json = JsonSerializer.Serialize(metadata, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file
                string outputPath = "metadata.json";
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Metadata extracted and saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }