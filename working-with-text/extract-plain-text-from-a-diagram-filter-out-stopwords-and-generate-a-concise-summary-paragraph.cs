using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
    {
        // Simple list of common English stopwords
        private static readonly HashSet<string> StopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "a","an","the","and","or","but","if","while","of","at","by","for","with","about","against",
            "between","into","through","during","before","after","above","below","to","from","up","down",
            "in","out","on","off","over","under","again","further","then","once","here","there","when",
            "where","why","how","all","any","both","each","few","more","most","other","some","such",
            "no","nor","not","only","own","same","so","than","too","very","can","will","just","don",
            "should","now"
        };

        static void Main(string[] args)
        {
            // Expect the diagram file path as the first argument
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DiagramTextSummarizer <path-to-visio-file>");
                return;
            }

            string diagramPath = args[0];
            if (!File.Exists(diagramPath))
            {
                Console.WriteLine($"File not found: {diagramPath}");
                return;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Collect all plain text from shapes
                StringBuilder allTextBuilder = new StringBuilder();

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve plain text; shape.Text.Value.Text concatenates all Txt runs
                        string shapeText = shape.Text.Value.Text;
                        if (!string.IsNullOrWhiteSpace(shapeText))
                        {
                            allTextBuilder.AppendLine(shapeText);
                        }
                    }
                }

                string allText = allTextBuilder.ToString();

                // Split text into words using regex, filter stopwords, and count frequencies
                Dictionary<string, int> wordFreq = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (Match match in Regex.Matches(allText, @"\b\w+\b"))
                {
                    string word = match.Value.ToLowerInvariant();
                    if (StopWords.Contains(word))
                        continue;

                    if (wordFreq.ContainsKey(word))
                        wordFreq[word]++;
                    else
                        wordFreq[word] = 1;
                }

                // Order words by frequency descending and take the top N words
                int topWordCount = 20;
                List<string> topWords = new List<string>();
                foreach (var kvp in wordFreq)
                {
                    // Simple insertion to keep list sorted by frequency
                    int index = topWords.FindIndex(w => wordFreq[w] < kvp.Value);
                    if (index == -1)
                        topWords.Add(kvp.Key);
                    else
                        topWords.Insert(index, kvp.Key);

                    if (topWords.Count > topWordCount)
                        topWords.RemoveAt(topWords.Count - 1);
                }

                // Build a concise summary paragraph from the top words
                string summary = "Summary: " + string.Join(" ", topWords) + ".";

                Console.WriteLine(summary);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while processing the diagram:");
                Console.WriteLine(ex.Message);
            }
        }
    }