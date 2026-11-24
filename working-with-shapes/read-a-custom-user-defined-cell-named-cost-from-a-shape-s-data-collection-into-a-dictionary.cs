using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Dictionary to hold Shape ID -> Cost value
        Dictionary<long, string> shapeCostMap = new Dictionary<long, string>();

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Look for a user-defined cell named "Cost"
                        foreach (User userCell in shape.Users)
                        {
                            // Compare the universal name (NameU) of the user cell
                            if (string.Equals(userCell.NameU, "Cost", StringComparison.OrdinalIgnoreCase))
                            {
                                // Ensure the shape ID is valid before adding to the dictionary
                                long shapeId = shape.ID;
                                string costValue = userCell.Value.Val ?? string.Empty;

                                // Add or update the entry in the dictionary
                                if (!shapeCostMap.ContainsKey(shapeId))
                                    shapeCostMap.Add(shapeId, costValue);
                                else
                                    shapeCostMap[shapeId] = costValue;

                                // Once found, no need to check other user cells for this shape
                                break;
                            }
                        }
                    }
                }
            }

            // Output the collected Cost values
            Console.WriteLine("Shape ID -> Cost values:");
            foreach (KeyValuePair<long, string> entry in shapeCostMap)
            {
                Console.WriteLine($"{entry.Key} -> {entry.Value}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}