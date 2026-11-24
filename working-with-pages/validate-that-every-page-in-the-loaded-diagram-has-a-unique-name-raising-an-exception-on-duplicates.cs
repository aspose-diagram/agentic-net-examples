using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure a file path argument is provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as an argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the file exists before attempting to load.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate that each page has a unique name.
                var pageNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the page name; handle possible nulls gracefully.
                    string name = page.Name ?? string.Empty;

                    // If the name already exists in the set, a duplicate is found.
                    if (pageNames.Contains(name))
                    {
                        throw new Exception($"Duplicate page name detected: '{name}'.");
                    }

                    pageNames.Add(name);
                }

                Console.WriteLine("All page names are unique.");
            }
        }
        catch (Exception ex)
        {
            // Output any errors encountered during loading or validation.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}