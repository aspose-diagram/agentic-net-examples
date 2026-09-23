using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output diagram file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Mapping of old author names to new author names
            var authorMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Alice", "Alice Smith" },
                { "Bob", "Robert Johnson" }
                // Add additional mappings as required
            };

            // Update reviewer (author) names according to the mapping
            foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
            {
                string currentName = reviewer.Name.Value;
                if (authorMapping.TryGetValue(currentName, out string newName))
                {
                    reviewer.Name.Value = newName;
                    Console.WriteLine($"Reviewer name updated: '{currentName}' -> '{newName}'");
                }
            }

            // Save the diagram with the updated author information
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
