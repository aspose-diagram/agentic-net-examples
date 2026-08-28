using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output CSV file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramCommentExport <inputVisioFile> <outputCsvFile>");
                return;
            }

            string inputPath = args[0];
            string outputCsvPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Build a lookup of reviewer IDs to reviewer names
            // Reviewer collection is accessed via diagram.DocumentSheet.Reviewers
            // Reviewer.Name is a Str2Value, retrieve the string via .Value
            var reviewerNames = new System.Collections.Generic.List<string>();
            foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
            {
                // Ensure the name is not null
                string name = reviewer.Name?.Value ?? string.Empty;
                reviewerNames.Add(name);
            }

            // Prepare the CSV file for writing (UTF-8 without BOM)
            using (var writer = new StreamWriter(outputCsvPath, false, new UTF8Encoding(false)))
            {
                // Write CSV header
                writer.WriteLine("CommentText,Author");

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Annotations (comments) are stored in the page's PageSheet
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve comment text
                        string commentText = annotation.Comment?.Value ?? string.Empty;

                        // Retrieve reviewer ID and map to reviewer name
                        int reviewerId = annotation.ReviewerID?.Value ?? -1;
                        string authorName = string.Empty;
                        if (reviewerId >= 0 && reviewerId < reviewerNames.Count)
                        {
                            authorName = reviewerNames[reviewerId];
                        }

                        // Escape CSV fields (double quotes are escaped by doubling them)
                        string escapedComment = $"\"{commentText.Replace("\"", "\"\"")}\"";
                        string escapedAuthor = $"\"{authorName.Replace("\"", "\"\"")}\"";

                        // Write the CSV line
                        writer.WriteLine($"{escapedComment},{escapedAuthor}");
                    }
                }
            }

            Console.WriteLine($"Comments exported successfully to '{outputCsvPath}'.");
        }
    }