using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the folder path containing Visio files as the first argument.");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Prepare a list to hold report rows
        var reportRows = new List<string>();
        // Add CSV header
        reportRows.Add("FilePath,PageName,CommentID,ReviewerID,CommentText,CommentDate");

        // Get Visio files (common extensions)
        string[] visioFiles = Directory.GetFiles(folderPath, "*.vsdx");
        // You can extend the pattern list if needed (e.g., *.vsd, *.vdx)

        foreach (string filePath in visioFiles)
        {
            // Guard: ensure the file exists (redundant after GetFiles but kept per rules)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Page name may be null; use a fallback
                    string pageName = page.Name ?? "Unnamed";

                    // Iterate through annotations (comments) on the page sheet
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve comment details
                        int commentId = annotation.MarkerIndex.Value;
                        int reviewerId = annotation.ReviewerID.Value;
                        string commentText = annotation.Comment.Value ?? string.Empty;
                        DateTime commentDate = annotation.Date?.Value ?? DateTime.MinValue;

                        // Format date as ISO 8601 for CSV
                        string dateString = commentDate == DateTime.MinValue ? "" : commentDate.ToString("o");

                        // Escape commas in text fields
                        string escapedText = commentText.Replace("\"", "\"\"");
                        if (escapedText.Contains(",") || escapedText.Contains("\""))
                        {
                            escapedText = $"\"{escapedText}\"";
                        }

                        // Build CSV row
                        string row = $"{filePath},{pageName},{commentId},{reviewerId},{escapedText},{dateString}";
                        reportRows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors encountered while processing the file
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }

        // Define output report path
        string reportPath = Path.Combine(folderPath, "CommentReport.csv");

        try
        {
            // Write all rows to the CSV file
            File.WriteAllLines(reportPath, reportRows);
            Console.WriteLine($"Comment report generated successfully at: {reportPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write report file: {ex.Message}");
        }
    }
}