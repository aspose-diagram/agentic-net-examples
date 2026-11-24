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
                // Dictionary to map shape IDs to the list of page names where they appear.
                Dictionary<long, List<string>> idToPages = new Dictionary<long, List<string>>();

                // Iterate through all pages and their shapes.
                foreach (Page page in diagram.Pages)
                {
                    // Ensure the page name is not null.
                    string pageName = page.Name ?? "UnnamedPage";

                    foreach (Shape shape in page.Shapes)
                    {
                        long shapeId = shape.ID;

                        if (!idToPages.ContainsKey(shapeId))
                        {
                            // First occurrence of this ID.
                            idToPages[shapeId] = new List<string> { pageName };
                        }
                        else
                        {
                            // Duplicate occurrence; add the page name to the list.
                            idToPages[shapeId].Add(pageName);
                        }
                    }
                }

                // Collect duplicate IDs.
                List<long> duplicateIds = new List<long>();
                foreach (KeyValuePair<long, List<string>> kvp in idToPages)
                {
                    if (kvp.Value.Count > 1)
                    {
                        duplicateIds.Add(kvp.Key);
                    }
                }

                // Report results.
                if (duplicateIds.Count == 0)
                {
                    Console.WriteLine("No duplicate shape IDs found.");
                }
                else
                {
                    Console.WriteLine("Duplicate shape IDs detected:");
                    foreach (long dupId in duplicateIds)
                    {
                        List<string> pages = idToPages[dupId];
                        Console.WriteLine($"- Shape ID {dupId} appears on pages: {string.Join(", ", pages)}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}