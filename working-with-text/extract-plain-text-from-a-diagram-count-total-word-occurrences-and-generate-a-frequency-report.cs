using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect at least one argument: the path to the Visio diagram file.
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DiagramWordFrequency <input-diagram-path> [output-report-path]");
                return;
            }

            string diagramPath = args[0];
            string? reportPath = args.Length > 1 ? args[1] : null;

            // Load the diagram.
            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Dictionary to hold word frequencies (case‑insensitive).
            var wordFreq = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            long totalWordCount = 0;

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve plain text from the shape.
                    string text = shape.Text.Value.Text;

                    if (string.IsNullOrWhiteSpace(text))
                        continue;

                    // Remove punctuation and normalize whitespace.
                    string cleaned = Regex.Replace(text, @"[^\w\s]", " ");
                    string[] words = cleaned.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string rawWord in words)
                    {
                        string word = rawWord.ToLowerInvariant();
                        if (wordFreq.ContainsKey(word))
                            wordFreq[word]++;
                        else
                            wordFreq[word] = 1;

                        totalWordCount++;
                    }
                }
            }

            // Prepare the report lines.
            var reportLines = new List<string>
            {
                $"Total words: {totalWordCount}",
                "Word frequencies (descending):"
            };

            foreach (var kvp in SortedByFrequency(wordFreq))
            {
                reportLines.Add($"{kvp.Key}: {kvp.Value}");
            }

            // Output to console.
            foreach (string line in reportLines)
            {
                Console.WriteLine(line);
            }

            // Optionally write the report to a file.
            if (!string.IsNullOrEmpty(reportPath))
            {
                try
                {
                    File.WriteAllLines(reportPath, reportLines);
                    Console.WriteLine($"Report saved to: {reportPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to write report file: {ex.Message}");
                }
            }
        }

        // Helper to sort the dictionary by descending frequency.
        private static IEnumerable<KeyValuePair<string, int>> SortedByFrequency(Dictionary<string, int> dict)
        {
            var list = new List<KeyValuePair<string, int>>(dict);
            list.Sort((a, b) => b.Value.CompareTo(a.Value));
            return list;
        }
    }