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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputSummaryFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Dictionary to hold master name -> shape count.
            Dictionary<string, int> masterCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine the master name; use "NoMaster" when the shape has no master.
                    string masterName = shape.Master != null ? shape.Master.Name : "NoMaster";

                    // Increment the count for this master.
                    if (masterCounts.ContainsKey(masterName))
                        masterCounts[masterName]++;
                    else
                        masterCounts[masterName] = 1;
                }
            }

            // Write the summary to the output text file.
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                foreach (KeyValuePair<string, int> kvp in masterCounts)
                {
                    writer.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }

            Console.WriteLine($"Summary written to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}