using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class DiagramSummarizer
{
    // List of common English stopwords
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

            // Path to the source Visio diagram file (VSDX, VSD, etc.)
            string diagramPath = "inputDiagram.vsdx";

            // Load the diagram using Aspose.Diagram
            Diagram diagram = new Diagram(diagramPath);

            // Extract all plain text from shapes across all pages
            List<string> allTexts = new List<string>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Some shapes may not contain text; guard against nulls
                    if (shape.Text != null && shape.Text.Value != null)
                    {
                        string text = shape.Text.Value.ToString();
                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            allTexts.Add(text);
                        }
                    }
                }
            }

            // Combine extracted texts into a single block
            string combinedText = string.Join(" ", allTexts);

            // Split the combined text into sentences (basic split on punctuation)
            string[] sentences = combinedText
                .Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToArray();

            // Process each sentence: remove stopwords and rebuild the sentence
            List<string> filteredSentences = new List<string>();
            foreach (string sentence in sentences)
            {
                string[] words = sentence.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                var filteredWords = words
                    .Where(w => !StopWords.Contains(w.Trim(new[] { ',', ';', ':' })))
                    .ToArray();

                if (filteredWords.Length > 0)
                {
                    // Capitalize first word and add a period at the end
                    string rebuilt = char.ToUpper(filteredWords[0][0]) + filteredWords[0].Substring(1);
                    if (filteredWords.Length > 1)
                    {
                        rebuilt += " " + string.Join(" ", filteredWords.Skip(1));
                    }
                    rebuilt += ".";
                    filteredSentences.Add(rebuilt);
                }
            }

            // Generate a concise summary: take the first two filtered sentences (or fewer if not available)
            string summary = string.Join(" ", filteredSentences.Take(2));

            // Output the summary
            Console.WriteLine("Summary:");
            Console.WriteLine(summary);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
