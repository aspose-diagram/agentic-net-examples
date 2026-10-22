using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // HashSet to track page names and detect duplicates.
                HashSet<string> pageNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // Iterate over each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the page name (string). If null, treat as empty.
                    string name = page.Name ?? string.Empty;

                    // Check for duplicate names.
                    if (!pageNames.Add(name))
                    {
                        // Duplicate found – raise an exception.
                        throw new Exception($"Duplicate page name detected: \"{name}\"");
                    }
                }

                // If execution reaches here, all page names are unique.
                Console.WriteLine("All page names are unique.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}