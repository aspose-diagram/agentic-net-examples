using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect all plain text from every shape in the diagram.
                StringBuilder allTextBuilder = new StringBuilder();

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the concatenated plain text of the shape.
                        string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                        // Skip empty or whitespace‑only text.
                        if (string.IsNullOrWhiteSpace(shapeText))
                            continue;

                        // Remove any HTML tags that might be present.
                        string cleanedText = Regex.Replace(shapeText, "<.*?>", string.Empty);

                        // Append a space to separate texts from different shapes.
                        allTextBuilder.Append(cleanedText).Append(' ');
                    }
                }

                string allText = allTextBuilder.ToString();

                if (string.IsNullOrWhiteSpace(allText))
                {
                    Console.WriteLine("No textual content found in the diagram.");
                    return;
                }

                // ---------- Stop‑word filtering ----------
                // Simple stop‑word list; can be extended as needed.
                HashSet<string> stopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "a","an","the","and","or","but","if","while","of","at","by","for","with",
                    "about","against","between","into","through","during","before","after",
                    "above","below","to","from","up","down","in","out","on","off","over",
                    "under","again","further","then","once","here","there","when","where",
                    "why","how","all","any","both","each","few","more","most","other",
                    "some","such","no","nor","not","only","own","same","so","than","too",
                    "very","can","will","just","don","should","now"
                };

                // Build word frequency dictionary from filtered words.
                Dictionary<string, int> wordFreq = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (Match match in Regex.Matches(allText, @"\b\w+\b"))
                {
                    string word = match.Value.ToLowerInvariant();
                    if (stopWords.Contains(word))
                        continue;

                    if (wordFreq.ContainsKey(word))
                        wordFreq[word]++;
                    else
                        wordFreq[word] = 1;
                }

                // ---------- Sentence scoring ----------
                // Split the text into sentences.
                string[] sentences = Regex.Split(allText, @"(?<=[\.!\?])\s+");

                // Score each sentence based on word frequencies.
                List<(string Sentence, double Score)> scoredSentences = new List<(string, double)>();
                foreach (string sentence in sentences)
                {
                    if (string.IsNullOrWhiteSpace(sentence))
                        continue;

                    double score = 0;
                    foreach (Match match in Regex.Matches(sentence, @"\b\w+\b"))
                    {
                        string word = match.Value.ToLowerInvariant();
                        if (stopWords.Contains(word))
                            continue;

                        if (wordFreq.TryGetValue(word, out int freq))
                            score += freq;
                    }

                    scoredSentences.Add((sentence.Trim(), score));
                }

                // Sort sentences by descending score.
                scoredSentences.Sort((a, b) => b.Score.CompareTo(a.Score));

                // Select top 3 sentences (or fewer if not enough).
                int summarySentenceCount = Math.Min(3, scoredSentences.Count);
                List<string> summarySentences = new List<string>();
                for (int i = 0; i < summarySentenceCount; i++)
                {
                    summarySentences.Add(scoredSentences[i].Sentence);
                }

                // Combine selected sentences into a concise summary paragraph.
                string summary = string.Join(" ", summarySentences);

                // Output the summary.
                Console.WriteLine("=== Summary ===");
                Console.WriteLine(summary);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}