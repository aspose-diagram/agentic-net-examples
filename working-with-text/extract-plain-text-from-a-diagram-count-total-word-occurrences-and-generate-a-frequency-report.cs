using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Input Visio file path (first argument or default)
            string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

            if (!File.Exists(diagramPath))
            {
                Console.WriteLine($"File not found: {diagramPath}");
                return;
            }

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Dictionary to hold word frequencies (case‑insensitive)
            var wordFreq = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Process each page and its shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    ProcessShape(shape, wordFreq);
                }
            }

            // Generate report: sort by descending frequency then alphabetically
            var reportLines = wordFreq
                .OrderByDescending(kv => kv.Value)
                .ThenBy(kv => kv.Key)
                .Select(kv => $"{kv.Key}: {kv.Value}")
                .ToList();

            // Output to console
            Console.WriteLine("Word Frequency Report:");
            foreach (string line in reportLines)
            {
                Console.WriteLine(line);
            }

            // Optionally write the report to a text file
            string reportPath = "frequency_report.txt";
            try
            {
                File.WriteAllLines(reportPath, reportLines);
                Console.WriteLine($"Report saved to {reportPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write report file: {ex.Message}");
            }
        }

        // Recursively extracts text from a shape (including group children) and updates the frequency dictionary
        private static void ProcessShape(Shape shape, Dictionary<string, int> wordFreq)
        {
            // Get plain text from the shape
            string text = shape.Text.Value.Text;

            if (!string.IsNullOrWhiteSpace(text))
            {
                // Use regex to match words (alphanumeric sequences)
                foreach (Match match in Regex.Matches(text, @"\b\w+\b"))
                {
                    string word = match.Value.ToLowerInvariant();
                    if (wordFreq.ContainsKey(word))
                        wordFreq[word]++;
                    else
                        wordFreq[word] = 1;
                }
            }

            // If the shape is a group, process its child shapes recursively
            if (shape.Type == TypeValue.Group && shape.Shapes != null)
            {
                foreach (Shape child in shape.Shapes)
                {
                    ProcessShape(child, wordFreq);
                }
            }
        }
    }