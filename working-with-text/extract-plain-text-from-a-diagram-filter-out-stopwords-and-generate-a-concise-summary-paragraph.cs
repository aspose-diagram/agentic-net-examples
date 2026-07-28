using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
    {
        // Simple list of common English stopwords
        static readonly HashSet<string> StopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
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

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Collect all plain text from the diagram
                StringBuilder allTextBuilder = new StringBuilder();
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        ExtractShapeText(shape, allTextBuilder);
                    }
                }

                string allText = allTextBuilder.ToString();

                // Generate summary
                string summary = GenerateSummary(allText);

                Console.WriteLine("=== Diagram Summary ===");
                Console.WriteLine(summary);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Recursively extracts text from a shape (including group shapes)
        static void ExtractShapeText(Shape shape, StringBuilder builder)
        {
            // Skip deleted shapes
            if (shape.Del == BOOL.True)
                return;

            // Get plain text of the shape
            string text = shape.Text.Value.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                // Normalize whitespace and append
                text = Regex.Replace(text, @"\s+", " ").Trim();
                builder.AppendLine(text);
            }

            // If the shape is a group, process its child shapes
            if (shape.Type == TypeValue.Group && shape.Shapes != null)
            {
                foreach (Shape child in shape.Shapes)
                {
                    ExtractShapeText(child, builder);
                }
            }
        }

        // Creates a concise summary based on word frequency
        static string GenerateSummary(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "No textual content found in the diagram.";

            // Split text into words, remove punctuation
            var words = Regex.Matches(text.ToLower(), @"\b[\w']+\b")
                             .Cast<Match>()
                             .Select(m => m.Value)
                             .Where(w => !StopWords.Contains(w));

            // Count frequencies
            var frequency = words.GroupBy(w => w)
                                 .Select(g => new { Word = g.Key, Count = g.Count() })
                                 .OrderByDescending(x => x.Count)
                                 .Take(10) // top 10 words
                                 .ToList();

            if (!frequency.Any())
                return "Content consists mainly of stopwords.";

            // Build summary sentence
            var topWords = frequency.Select(f => f.Word).ToArray();
            string summary = $"Key topics in the diagram include: {string.Join(", ", topWords)}.";
            return summary;
        }
    }