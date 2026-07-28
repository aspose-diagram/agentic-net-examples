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
            try
            {

                // Path to the Visio file to be processed
                string inputPath = "input.vsdx";

                // Path where the extracted metadata JSON will be saved
                string outputPath = "metadata.json";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Retrieve built‑in document properties
                    string title = diagram.DocumentProps.Title;
                    string author = diagram.DocumentProps.Creator;
                    DateTime creationDate = diagram.DocumentProps.TimeCreated;

                    // Assemble metadata into a dictionary
                    var metadata = new Dictionary<string, object>
                    {
                        { "Title", title },
                        { "Author", author },
                        { "CreationDate", creationDate }
                    };

                    // Convert the metadata dictionary to a formatted JSON string
                    string json = JsonSerializer.Serialize(metadata, new JsonSerializerOptions { WriteIndented = true });

                    // Write the JSON string to the output file
                    File.WriteAllText(outputPath, json);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }