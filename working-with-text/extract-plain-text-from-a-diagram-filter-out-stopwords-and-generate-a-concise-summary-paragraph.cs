using System;
using System.Collections.Generic;
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
            try
            {

                // Path to the Visio diagram; can be passed as a command‑line argument
                string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Collect raw text from all shapes
                List<string> rawTexts = new List<string>();
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        ExtractShapeText(shape, rawTexts);
                    }
                }

                // Combine all extracted texts into a single string
                string combinedText = string.Join(" ", rawTexts);

                // Remove punctuation
                string cleanedText = Regex.Replace(combinedText, @"[^\w\s]", " ");

                // Split into words and filter stopwords
                string[] words = cleanedText.Split(new[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> filteredWords = new List<string>();
                foreach (string word in words)
                {
                    if (!StopWords.Contains(word))
                    {
                        filteredWords.Add(word);
                    }
                }

                // Build a concise summary (first 100 words or fewer)
                int summaryWordCount = Math.Min(100, filteredWords.Count);
                string summary = string.Join(" ", filteredWords.GetRange(0, summaryWordCount));

                // Output the summary
                Console.WriteLine("=== Diagram Summary ===");
                Console.WriteLine(summary);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }

        // Recursively extracts text from a shape and its child shapes (if any)
        private static void ExtractShapeText(Shape shape, List<string> collector)
        {
            // Skip deleted shapes
            if (shape.Del == BOOL.True)
                return;

            // Get plain text of the shape
            string text = shape.Text.Value.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                collector.Add(text);
            }

            // If the shape is a group, process its child shapes
            if (shape.Type == TypeValue.Group)
            {
                foreach (Shape child in shape.Shapes)
                {
                    ExtractShapeText(child, collector);
                }
            }
        }
    }