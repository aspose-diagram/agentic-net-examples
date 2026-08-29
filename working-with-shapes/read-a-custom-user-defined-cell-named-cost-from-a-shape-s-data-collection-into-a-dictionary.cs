using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio diagram file
        string diagramPath = "input.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Dictionary to store shape ID (long) and its "Cost" user-defined cell value
        Dictionary<long, string> costValues = new Dictionary<long, string>();

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate through user-defined cells (Users collection) of the shape
                    foreach (User userCell in shape.Users)
                    {
                        // Check if the user-defined cell name matches "Cost"
                        if (userCell.Name == "Cost" || userCell.NameU == "Cost")
                        {
                            // Store the value in the dictionary using the shape ID as the key
                            costValues[shape.ID] = userCell.Value.Val;
                            // Break after finding the first "Cost" cell for this shape
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Output the collected values
        foreach (var kvp in costValues)
        {
            Console.WriteLine($"Shape ID: {kvp.Key}, Cost: {kvp.Value}");
        }

        // Keep console window open if needed
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}