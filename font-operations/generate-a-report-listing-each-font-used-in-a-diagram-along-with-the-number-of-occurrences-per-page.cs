using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as an argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: check if the file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Dictionary to hold font usage counts for the current page
                Dictionary<string, int> fontCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate over all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate over character runs within the shape
                    for (int i = 0; i < shape.Chars.Count; i++)
                    {
                        // Use fully qualified type to avoid ambiguity with System.Char
                        Aspose.Diagram.Char ch = shape.Chars[i];

                        // Retrieve the font name from the character's FontName cell
                        string fontName = ch.FontName?.Value;

                        if (!string.IsNullOrEmpty(fontName))
                        {
                            if (fontCounts.ContainsKey(fontName))
                                fontCounts[fontName]++;
                            else
                                fontCounts[fontName] = 1;
                        }
                    }
                }

                // Output the report for the current page
                Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}");
                if (fontCounts.Count == 0)
                {
                    Console.WriteLine("  No fonts found on this page.");
                }
                else
                {
                    foreach (KeyValuePair<string, int> kvp in fontCounts)
                    {
                        Console.WriteLine($"  Font: {kvp.Key}, Occurrences: {kvp.Value}");
                    }
                }

                Console.WriteLine(); // Blank line between pages
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}