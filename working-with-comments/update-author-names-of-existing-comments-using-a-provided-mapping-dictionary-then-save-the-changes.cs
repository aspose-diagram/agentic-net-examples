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
                    { "Alice", "Alice Johnson" },
                    { "Bob", "Robert Smith" },
                    { "Charlie", "Charles Brown" }
                };

                // Load the existing diagram
                var diagram = new Diagram(inputPath);

                // Iterate through the reviewers collection and update names based on the mapping
                foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
                {
                    string currentName = reviewer.Name.Value;
                    if (authorMapping.TryGetValue(currentName, out string newName))
                    {
                        reviewer.Name.Value = newName;
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }