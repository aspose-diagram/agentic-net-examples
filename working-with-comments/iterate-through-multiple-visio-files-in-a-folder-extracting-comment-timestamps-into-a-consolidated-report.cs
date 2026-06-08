using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing Visio files (change as needed)
            string folderPath = @"C:\VisioFiles";

            // Output report file
            string reportPath = Path.Combine(folderPath, "CommentTimestampsReport.csv");

            // List to hold report lines
            List<string> reportLines = new List<string>();
            // Header for CSV
            reportLines.Add("FileName,PageName,CommentID,Timestamp,CommentText");

            // Get all Visio files in the folder (supports .vsdx, .vsd, .vdx, etc.)
            string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx" && extension != ".vsdm" && extension != ".vssx")
                {
                    continue; // Skip non‑Visio files
                }

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    // Access annotations (comments) via the PageSheet
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Unique identifier for the comment
                        long commentId = annotation.MarkerIndex.Value;

                        // Timestamp – assuming the Annotation class provides a Date property
                        // If the property does not exist, this line should be adjusted accordingly.
                        DateTime timestamp = annotation.Date.Value;

                        // Comment text
                        string commentText = annotation.Comment.Value;

                        // Build CSV line (escape commas in text)
                        string escapedText = commentText.Replace("\"", "\"\"");
                        string line = $"{Path.GetFileName(filePath)},{page.Name},{commentId},{timestamp:o},\"{escapedText}\"";
                        reportLines.Add(line);
                    }
                }
            }

            // Write the consolidated report
            File.WriteAllLines(reportPath, reportLines, Encoding.UTF8);
            Console.WriteLine($"Report generated at: {reportPath}");
        }
    }