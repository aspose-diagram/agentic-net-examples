using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Dictionary to store master name -> usage count
                Dictionary<string, int> masterUsage = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate over all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape is associated with a master
                        if (shape.Master == null)
                            continue;

                        // Retrieve the master name
                        string masterName = shape.Master.Name;

                        // Increment the usage count for this master
                        if (masterUsage.ContainsKey(masterName))
                            masterUsage[masterName]++;
                        else
                            masterUsage[masterName] = 1;
                    }
                }

                // Output the master usage report to the console
                Console.WriteLine("Master Usage Report:");
                Console.WriteLine("---------------------");
                foreach (KeyValuePair<string, int> entry in masterUsage)
                {
                    Console.WriteLine($"Master: {entry.Key}, Usage Count: {entry.Value}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}