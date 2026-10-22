using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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

        // Pre‑populate the author name mapping dictionary.
        var authorMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Alice", "Alice Smith" },
            { "Bob", "Robert Johnson" }
        };

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Build a lookup of reviewer IDs to new author names.
                // Iterate over the reviewers collection and update names where a mapping exists.
                foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
                {
                    // Retrieve the current author name.
                    string currentName = reviewer.Name?.Value ?? string.Empty;

                    // If the current name exists in the mapping, update it.
                    if (authorMapping.TryGetValue(currentName, out string newName))
                    {
                        // Update the reviewer name in the diagram.
                        reviewer.Name.Value = newName;
                    }
                }

                // No further changes to annotations are required because they reference reviewers by ID.
                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}