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
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Dictionary to hold word frequencies (case‑insensitive).
            var wordFreq = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Regular expression to match words (alphanumeric sequences).
            Regex wordRegex = new Regex(@"\b\w+\b", RegexOptions.Compiled);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve plain text from the shape.
                    string rawText = shape.Text?.Value?.Text ?? string.Empty;

                    // Skip shapes without text.
                    if (string.IsNullOrWhiteSpace(rawText))
                        continue;

                    // Remove any HTML tags that might be present.
                    string cleanText = Regex.Replace(rawText, "<.*?>", string.Empty);

                    // Find all word matches.
                    foreach (Match match in wordRegex.Matches(cleanText))
                    {
                        string word = match.Value;
                        if (wordFreq.ContainsKey(word))
                            wordFreq[word]++;
                        else
                            wordFreq[word] = 1;
                    }
                }
            }

            // Prepare the frequency report.
            var reportLines = new List<string>();
            reportLines.Add("Word Frequency Report");
            reportLines.Add("=====================");
            foreach (var kvp in SortedByCountDescending(wordFreq))
            {
                reportLines.Add($"{kvp.Key}: {kvp.Value}");
            }

            // Output the report to the console.
            foreach (string line in reportLines)
            {
                Console.WriteLine(line);
            }

            // Also write the report to a text file in the same directory as the diagram.
            string reportPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "frequency_report.txt");
            File.WriteAllLines(reportPath, reportLines);
            Console.WriteLine($"\nReport saved to: {reportPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Helper method to sort dictionary entries by descending count.
    private static IEnumerable<KeyValuePair<string, int>> SortedByCountDescending(Dictionary<string, int> dict)
    {
        var list = new List<KeyValuePair<string, int>>(dict);
        list.Sort((a, b) => b.Value.CompareTo(a.Value));
        return list;
    }
}