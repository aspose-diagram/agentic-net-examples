using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Prompt user for the Visio diagram file path
            Console.Write("Enter the path to the Visio diagram file: ");
            string diagramPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(diagramPath))
            {
                Console.WriteLine("Invalid file path.");
                return;
            }

            // Load the diagram
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

            // Dictionary to hold word frequencies (case‑insensitive)
            var wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            int totalWordCount = 0;

            // Regular expression to match words (alphanumeric sequences)
            Regex wordRegex = new Regex(@"\b\w+\b", RegexOptions.Compiled);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve plain text from the shape
                    string text = shape.Text.Value.Text;

                    if (string.IsNullOrWhiteSpace(text))
                        continue; // Skip shapes without text

                    // Find all word matches
                    MatchCollection matches = wordRegex.Matches(text);
                    foreach (Match match in matches)
                    {
                        string word = match.Value;
                        if (wordCounts.ContainsKey(word))
                            wordCounts[word]++;
                        else
                            wordCounts[word] = 1;

                        totalWordCount++;
                    }
                }
            }

            // Output total word count
            Console.WriteLine($"\nTotal words found: {totalWordCount}\n");

            // Generate frequency report sorted by descending count
            var sortedWordCounts = wordCounts.OrderByDescending(kvp => kvp.Value)
                                             .ThenBy(kvp => kvp.Key);

            Console.WriteLine("Word Frequency Report:");
            foreach (var kvp in sortedWordCounts)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
    }