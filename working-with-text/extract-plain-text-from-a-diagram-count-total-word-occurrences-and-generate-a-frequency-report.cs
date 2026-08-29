using System.IO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Get the diagram file path from command line or prompt the user
            string diagramPath;
            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                diagramPath = args[0];
            }
            else
            {
                Console.Write("Enter the path to the Visio diagram file: ");
                diagramPath = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(diagramPath))
            {
                Console.WriteLine("No diagram path provided. Exiting.");
                return;
            }

            // Load the diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Dictionary to hold word frequencies (case‑insensitive)
                var wordFreq = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                long totalWordCount = 0;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve plain text from the shape
                        string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                        // Remove any non‑letter/digit characters (keep spaces)
                        string cleaned = Regex.Replace(shapeText, @"[^\w\s]", " ");

                        // Split into words based on whitespace
                        string[] words = cleaned.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string word in words)
                        {
                            // Update total count
                            totalWordCount++;

                            // Update frequency dictionary
                            if (wordFreq.ContainsKey(word))
                                wordFreq[word]++;
                            else
                                wordFreq[word] = 1;
                        }
                    }
                }

                // Output results
                Console.WriteLine($"Total words found: {totalWordCount}");
                Console.WriteLine();
                Console.WriteLine("Word Frequency Report (descending order):");
                Console.WriteLine("----------------------------------------");

                // Sort by frequency descending, then alphabetically
                foreach (var kvp in SortedByFrequency(wordFreq))
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }

    // Helper method to sort the dictionary by frequency descending
    private static IEnumerable<KeyValuePair<string, int>> SortedByFrequency(Dictionary<string, int> dict)
    {
        var list = new List<KeyValuePair<string, int>>(dict);
        list.Sort((a, b) =>
        {
            int cmp = b.Value.CompareTo(a.Value); // descending frequency
            if (cmp == 0)
                cmp = string.Compare(a.Key, b.Key, StringComparison.OrdinalIgnoreCase); // alphabetical
            return cmp;
        });
        return list;
    }
}
