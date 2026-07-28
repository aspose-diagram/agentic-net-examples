using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output CSV file path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramCommentsExport <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputCsvPath = args[1];

        // Load the diagram
        using (Diagram diagram = new Diagram(inputPath))
        {
            // Prepare CSV lines with header
            List<string> csvLines = new List<string>();
            csvLines.Add("CommentId,CommentText,Author");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all annotations (comments) on the page
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    long commentId = annotation.MarkerIndex.Value;
                    string commentText = annotation.Comment.Value;

                    // Retrieve author name based on ReviewerID
                    int reviewerId = annotation.ReviewerID.Value;
                    string authorName = GetReviewerName(reviewerId, diagram);

                    // Escape commas in text fields
                    string escapedText = commentText.Replace("\"", "\"\"");
                    string escapedAuthor = authorName.Replace("\"", "\"\"");

                    // Build CSV line
                    string csvLine = $"{commentId},\"{escapedText}\",\"{escapedAuthor}\"";
                    csvLines.Add(csvLine);
                }
            }

            // Write all lines to the CSV file
            File.WriteAllLines(outputCsvPath, csvLines);
            Console.WriteLine($"Export completed. {csvLines.Count - 1} comment(s) written to '{outputCsvPath}'.");
        }
    }

    // Helper method to map ReviewerID to reviewer name
    private static string GetReviewerName(int reviewerId, Diagram diagram)
    {
        int index = 0;
        foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
        {
            if (index == reviewerId)
            {
                // Reviewer.Name is a Str2Value; use .Value to get the string
                return reviewer.Name.Value;
            }
            index++;
        }
        return "Unknown";
    }
}
