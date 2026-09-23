using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (modify as needed)
                string diagramPath = "input.vsdx";

                // Output CSV file path
                string csvPath = "comments.csv";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Build a map of reviewer IDs to reviewer names
                Dictionary<int, string> reviewerMap = new Dictionary<int, string>();
                for (int i = 0; i < diagram.DocumentSheet.Reviewers.Count; i++)
                {
                    Reviewer reviewer = diagram.DocumentSheet.Reviewers[i];
                    // Reviewer.Name is a Str2Value; use .Value to get the string
                    reviewerMap[i] = reviewer.Name.Value;
                }

                // Write comments to CSV
                using (StreamWriter writer = new StreamWriter(csvPath))
                {
                    // CSV header
                    writer.WriteLine("CommentId,Author,Text");

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Annotations (comments) are stored in the page's PageSheet
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            int commentId = annotation.MarkerIndex.Value;
                            string commentText = annotation.Comment.Value ?? string.Empty;

                            // Retrieve author name using ReviewerID
                            string author = string.Empty;
                            int reviewerId = annotation.ReviewerID.Value;
                            if (reviewerMap.TryGetValue(reviewerId, out string name))
                            {
                                author = name;
                            }

                            // Escape fields for CSV compliance
                            string escapedAuthor = EscapeCsv(author);
                            string escapedText = EscapeCsv(commentText);

                            // Write the CSV line
                            writer.WriteLine($"{commentId},{escapedAuthor},{escapedText}");
                        }
                    }
                }

                Console.WriteLine($"Comments exported to '{csvPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to escape CSV fields
        private static string EscapeCsv(string field)
        {
            if (field == null)
                return string.Empty;

            bool containsSpecial = field.Contains("\"") || field.Contains(",") || field.Contains("\n") || field.Contains("\r");
            if (containsSpecial)
            {
                // Escape double quotes by doubling them
                string escaped = field.Replace("\"", "\"\"");
                return $"\"{escaped}\"";
            }
            else
            {
                return field;
            }
        }
    }