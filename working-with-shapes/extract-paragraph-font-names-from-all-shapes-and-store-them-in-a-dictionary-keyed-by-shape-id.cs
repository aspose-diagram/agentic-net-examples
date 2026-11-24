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
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the file exists
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(filePath);

            // Dictionary to hold shape ID -> set of font IDs used in that shape
            Dictionary<long, HashSet<int>> shapeFontIds = new Dictionary<long, HashSet<int>>();

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has character runs
                    if (shape.Chars == null || shape.Chars.Count == 0)
                        continue;

                    // Initialize set for this shape if not already present
                    if (!shapeFontIds.ContainsKey(shape.ID))
                        shapeFontIds[shape.ID] = new HashSet<int>();

                    // Iterate over each character run to collect font IDs
                    for (int i = 0; i < shape.Chars.Count; i++)
                    {
                        // Font property holds a reference to a font defined in the document
                        // The Value is an integer font ID
                        int fontId = (int)shape.Chars[i].Font.Value;
                        shapeFontIds[shape.ID].Add(fontId);
                    }
                }
            }

            // Convert the collected font IDs to a readable dictionary (shape ID -> comma‑separated font IDs)
            Dictionary<long, string> shapeFontNames = new Dictionary<long, string>();
            foreach (var kvp in shapeFontIds)
            {
                string fontsCsv = string.Join(",", kvp.Value);
                shapeFontNames[kvp.Key] = fontsCsv;
            }

            // Output the results
            Console.WriteLine("ShapeID -> FontIDs");
            foreach (var kvp in shapeFontNames)
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}