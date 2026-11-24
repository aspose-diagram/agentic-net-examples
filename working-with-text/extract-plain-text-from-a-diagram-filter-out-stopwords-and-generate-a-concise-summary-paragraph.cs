using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // List to collect raw text from all shapes
                List<string> extractedTexts = new List<string>();

                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve plain text from the shape
                        string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                        // Skip empty or whitespace-only text
                        if (string.IsNullOrWhiteSpace(shapeText))
                            continue;

                        // Remove any HTML tags that might be present
                        string cleanedText = Regex.Replace(shapeText, "<.*?>", string.Empty);

                        // Add cleaned text to the collection
                        extractedTexts.Add(cleanedText);
                    }
                }

                // Combine all extracted texts into a single string
                string allText = string.Join(" ", extractedTexts);

                // Simple stopword list (can be extended as needed)
                HashSet<string> stopwords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "a","an","the","and","or","but","if","while","of","at","by","for","with","about",
                    "against","between","into","through","during","before","after","above","below",
                    "to","from","up","down","in","out","on","off","over","under","again","further",
                    "then","once","here","there","when","where","why","how","all","any","both",
                    "each","few","more","most","other","some","such","no","nor","not","only",
                    "own","same","so","than","too","very","can","will","just","don","should","now"
                };

                // Split text into words, filter stopwords, and count frequencies
                var wordFrequencies = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (Match match in Regex.Matches(allText, @"\b\w+\b"))
                {
                    string word = match.Value.ToLowerInvariant();
                    if (stopwords.Contains(word))
                        continue;

                    if (wordFrequencies.ContainsKey(word))
                        wordFrequencies[word]++;
                    else
                        wordFrequencies[word] = 1;
                }

                // Select top N frequent words for the summary
                int topN = 10;
                var topWords = wordFrequencies
                               .OrderByDescending(kvp => kvp.Value)
                               .Take(topN)
                               .Select(kvp => kvp.Key)
                               .ToList();

                // Build a concise summary paragraph
                string summary = topWords.Count > 0
                                 ? $"Summary: {string.Join(", ", topWords)}."
                                 : "No significant text content found to generate a summary.";

                // Output the summary
                Console.WriteLine(summary);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}