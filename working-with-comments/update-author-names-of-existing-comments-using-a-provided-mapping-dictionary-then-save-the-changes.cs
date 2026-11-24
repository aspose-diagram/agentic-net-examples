using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Prepare author name mapping (sample entries as required)
        var authorMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Alice", "Alice Smith" },
            { "Bob", "Robert Johnson" }
        };

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over reviewers to update author names based on the mapping
                foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
                {
                    // Retrieve current author name safely
                    string currentName = reviewer.Name?.Value;
                    if (!string.IsNullOrEmpty(currentName) && authorMapping.TryGetValue(currentName, out string newName))
                    {
                        // Update the reviewer name
                        reviewer.Name.Value = newName;
                    }
                }

                // Save the updated diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}