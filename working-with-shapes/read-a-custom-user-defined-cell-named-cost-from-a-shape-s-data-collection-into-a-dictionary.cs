using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Dictionary to hold shape ID -> Cost value.
        var costDictionary = new Dictionary<long, string>();

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate through user‑defined cells (Users collection).
                        foreach (User userCell in shape.Users)
                        {
                            // Check for the custom cell named "Cost".
                            if (string.Equals(userCell.NameU, "Cost", StringComparison.OrdinalIgnoreCase))
                            {
                                // Store the value in the dictionary using the shape's ID as the key.
                                costDictionary[shape.ID] = userCell.Value.Val;
                                // No need to continue scanning other user cells for this shape.
                                break;
                            }
                        }
                    }
                }
            }

            // Output the collected Cost values.
            Console.WriteLine("Shape ID -> Cost values:");
            foreach (var kvp in costDictionary)
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}