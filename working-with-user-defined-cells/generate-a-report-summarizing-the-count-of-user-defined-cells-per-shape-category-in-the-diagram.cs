using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No diagram file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Dictionary to hold the total count of user-defined cells per shape category.
            Dictionary<string, int> categoryUserCellCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Determine the shape category.
                    // Use the master shape's NameU if available; otherwise, label as "NoMaster".
                    string category = shape.Master != null ? shape.Master.NameU : "NoMaster";

                    // Ensure the Users collection is not null before accessing Count.
                    int userCellCount = shape.Users != null ? shape.Users.Count : 0;

                    // Accumulate the count for this category.
                    if (categoryUserCellCounts.ContainsKey(category))
                    {
                        categoryUserCellCounts[category] += userCellCount;
                    }
                    else
                    {
                        categoryUserCellCounts[category] = userCellCount;
                    }
                }
            }

            // Output the report.
            Console.WriteLine("User-Defined Cells Count per Shape Category:");
            Console.WriteLine("------------------------------------------------");
            foreach (var kvp in categoryUserCellCounts)
            {
                Console.WriteLine($"Category: {kvp.Key}, User-Defined Cells: {kvp.Value}");
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}