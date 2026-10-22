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
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard: check if the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate over each page in the diagram
            int pageNumber = 1;
            foreach (Page page in diagram.Pages)
            {
                // Dictionary to hold font usage counts for the current page (case‑insensitive)
                Dictionary<string, int> fontUsage = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                // Iterate over each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Iterate over each character in the shape's text
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        // Retrieve the font name used by the character
                        string fontName = ch.FontName.Value;

                        // If the font name is valid, update the count
                        if (!string.IsNullOrEmpty(fontName))
                        {
                            if (fontUsage.ContainsKey(fontName))
                                fontUsage[fontName]++;
                            else
                                fontUsage[fontName] = 1;
                        }
                    }
                }

                // Output the report for the current page
                Console.WriteLine($"Page {pageNumber}:");
                if (fontUsage.Count == 0)
                {
                    Console.WriteLine("  No fonts found on this page.");
                }
                else
                {
                    foreach (KeyValuePair<string, int> entry in fontUsage)
                    {
                        Console.WriteLine($"  Font: {entry.Key}, Occurrences: {entry.Value}");
                    }
                }

                Console.WriteLine(); // Blank line between pages
                pageNumber++;
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}