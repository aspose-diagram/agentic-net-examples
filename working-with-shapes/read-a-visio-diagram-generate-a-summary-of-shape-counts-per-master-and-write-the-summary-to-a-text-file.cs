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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputSummaryPath>");
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

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Dictionary to hold master name -> shape count
            Dictionary<string, int> masterCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages
            for (int p = 0; p < diagram.Pages.Count; p++)
            {
                Page page = diagram.Pages[p];

                // Iterate through all shapes on the page
                for (int s = 0; s < page.Shapes.Count; s++)
                {
                    Shape shape = page.Shapes[s];

                    // Determine master name; use "NoMaster" for shapes without a master
                    string masterName = shape.Master != null && !string.IsNullOrEmpty(shape.Master.Name)
                        ? shape.Master.Name
                        : "NoMaster";

                    // Increment count for this master
                    if (masterCounts.ContainsKey(masterName))
                        masterCounts[masterName]++;
                    else
                        masterCounts[masterName] = 1;
                }
            }

            // Write the summary to the output text file
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                writer.WriteLine("Shape Count Summary per Master:");
                foreach (var kvp in masterCounts)
                {
                    writer.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }

            Console.WriteLine($"Summary written to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}