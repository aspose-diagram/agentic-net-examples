using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <diagram-file-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Dictionary to hold word frequencies.
        var wordFreq = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        int totalWordCount = 0;

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve plain text from the shape.
                    string rawText = shape.Text.Value.Text;

                    // Skip shapes without text.
                    if (string.IsNullOrWhiteSpace(rawText))
                        continue;

                    // Remove any HTML tags that might be present.
                    string plainText = Regex.Replace(rawText, "<.*?>", string.Empty);

                    // Split the text into words using a regex that matches word characters.
                    foreach (Match match in Regex.Matches(plainText, @"\b\w+\b"))
                    {
                        string word = match.Value;

                        // Update total count.
                        totalWordCount++;

                        // Update frequency dictionary.
                        if (wordFreq.ContainsKey(word))
                            wordFreq[word]++;
                        else
                            wordFreq[word] = 1;
                    }
                }
            }

            // Output total word count.
            Console.WriteLine($"Total Words: {totalWordCount}");
            Console.WriteLine();
            Console.WriteLine("Word Frequency Report (descending):");

            // Sort frequencies descending and write to console.
            foreach (var kvp in SortedByValueDesc(wordFreq))
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            // Optionally, write the report to a text file in the same folder as the diagram.
            string reportPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "frequency_report.txt");
            using (StreamWriter writer = new StreamWriter(reportPath))
            {
                writer.WriteLine($"Total Words: {totalWordCount}");
                writer.WriteLine();
                writer.WriteLine("Word Frequency Report (descending):");
                foreach (var kvp in SortedByValueDesc(wordFreq))
                {
                    writer.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Frequency report saved to: {reportPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to sort dictionary by value descending.
    private static IEnumerable<KeyValuePair<string, int>> SortedByValueDesc(Dictionary<string, int> dict)
    {
        var list = new List<KeyValuePair<string, int>>(dict);
        list.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
        return list;
    }
}