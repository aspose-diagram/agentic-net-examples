using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 1)
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
                // Dictionary to count occurrences of each shape ID.
                Dictionary<long, int> idCounts = new Dictionary<long, int>();

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        long shapeId = shape.ID;

                        // Increment count for this ID.
                        if (idCounts.ContainsKey(shapeId))
                            idCounts[shapeId]++;
                        else
                            idCounts[shapeId] = 1;
                    }
                }

                // Collect duplicate IDs.
                List<long> duplicateIds = new List<long>();
                foreach (KeyValuePair<long, int> kvp in idCounts)
                {
                    if (kvp.Value > 1)
                        duplicateIds.Add(kvp.Key);
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
                        Console.WriteLine($"- Shape ID: {dupId} (occurs {idCounts[dupId]} times)");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}