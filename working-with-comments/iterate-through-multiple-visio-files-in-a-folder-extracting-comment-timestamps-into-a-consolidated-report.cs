using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

namespace VisioCommentExtractor
{
    // Simple DTO to hold extracted comment information
    public class CommentRecord
    {
        public string FileName { get; set; } = string.Empty;
        public int PageId { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Determine the folder to scan
            string folderPath;
            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                folderPath = args[0];
            }
            else
            {
                Console.WriteLine("Please provide a valid folder path as the first argument.");
                return;
            }

            // Prepare a list to collect all comment records
            List<CommentRecord> allComments = new List<CommentRecord>();

            // Get all Visio files in the folder (supports common extensions)
            string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                // Guard to ensure the file actually exists before processing
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"File not found: {filePath}");
                    continue;
                }

                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                {
                    continue; // Skip non‑Visio files
                }

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through each page
                    foreach (Page page in diagram.Pages)
                    {
                        // Access annotations (comments) on the page
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            // Extract comment text
                            string commentText = annotation.Comment?.Value ?? string.Empty;

                            // Extract timestamp (Date). If Date is not set, fallback to empty string.
                            string timestamp = string.Empty;
                            if (annotation.Date != null)
                            {
                                // Date is stored as a DateTime; format it as ISO 8601.
                                DateTime dt = annotation.Date.Value;
                                timestamp = dt.ToString("o");
                            }

                            // Add record to the list
                            allComments.Add(new CommentRecord
                            {
                                FileName = Path.GetFileName(filePath),
                                PageId = page.ID,
                                CommentText = commentText,
                                Timestamp = timestamp
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log any errors encountered while processing a file
                    Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            // Output consolidated report as CSV to console
            Console.WriteLine("FileName,PageId,Timestamp,CommentText");
            foreach (CommentRecord record in allComments)
            {
                // Escape double quotes in comment text
                string escapedComment = record.CommentText.Replace("\"", "\"\"");
                Console.WriteLine($"\"{record.FileName}\",{record.PageId},\"{record.Timestamp}\",\"{escapedComment}\"");
            }
        }
    }
}