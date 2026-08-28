using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Mapping of old author names to new author names
                var authorMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Alice", "Alicia" },
                    { "Bob", "Robert" },
                    { "Charlie", "Charles" }
                    // Add more mappings as needed
                };

                // Load the existing diagram
                var diagram = new Diagram(inputPath);

                // Access the reviewers collection (authors of comments)
                var reviewers = diagram.DocumentSheet.Reviewers;

                // Update reviewer names based on the provided mapping
                for (int i = 0; i < reviewers.Count; i++)
                {
                    var reviewer = reviewers[i];
                    string currentName = reviewer.Name.Value;

                    if (authorMapping.TryGetValue(currentName, out string newName))
                    {
                        reviewer.Name.Value = newName;
                        Console.WriteLine($"Reviewer name updated: '{currentName}' -> '{newName}'");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }