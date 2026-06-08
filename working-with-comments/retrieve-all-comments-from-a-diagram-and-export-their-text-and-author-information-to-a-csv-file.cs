using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Path to the output CSV file
                string csvPath = "comments.csv";

                // Load the diagram
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Prepare the CSV file
                    using (StreamWriter writer = new StreamWriter(csvPath, false))
                    {
                        // Write CSV header
                        writer.WriteLine("CommentID,ReviewerID,Text");

                        // Iterate through all pages
                        foreach (Page page in diagram.Pages)
                        {
                            // Access annotations (comments) via the PageSheet
                            foreach (Annotation comment in page.PageSheet.Annotations)
                            {
                                // Retrieve comment identifier
                                long commentId = comment.MarkerIndex.Value;

                                // Retrieve reviewer identifier (author)
                                int reviewerId = comment.ReviewerID.Value;

                                // Retrieve comment text
                                string text = comment.Comment.Value ?? string.Empty;

                                // Clean text for CSV (replace newlines and commas)
                                text = text.Replace("\r", " ").Replace("\n", " ").Replace(",", " ");

                                // Write CSV line
                                writer.WriteLine($"{commentId},{reviewerId},{text}");
                            }
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
    }