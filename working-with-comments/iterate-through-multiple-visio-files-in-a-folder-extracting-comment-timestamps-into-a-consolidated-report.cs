using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to scan. Use the first argument if provided, otherwise the current directory.
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return;
            }

            // Prepare a list to hold report rows.
            List<string> reportLines = new List<string>();
            // Add CSV header.
            reportLines.Add("FileName,PageName,CommentID,ReviewerID,CommentDate,CommentText");

            // Get all Visio files in the folder (common extensions).
            string[] visioFiles = Directory.GetFiles(folderPath);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath);
                if (!extension.Equals(".vsdx", StringComparison.OrdinalIgnoreCase) &&
                    !extension.Equals(".vsd", StringComparison.OrdinalIgnoreCase) &&
                    !extension.Equals(".vssx", StringComparison.OrdinalIgnoreCase) &&
                    !extension.Equals(".vss", StringComparison.OrdinalIgnoreCase) &&
                    !extension.Equals(".vtx", StringComparison.OrdinalIgnoreCase))
                {
                    // Skip non‑Visio files.
                    continue;
                }

                try
                {
                    // Load the Visio diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through each page.
                    foreach (Page page in diagram.Pages)
                    {
                        // Access the annotations collection on the page sheet.
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            // Retrieve comment details.
                            long commentId = annotation.MarkerIndex.Value;
                            int reviewerId = annotation.ReviewerID.Value;
                            // Date and EditDate are read‑only; use the Date value if present.
                            string commentDate = annotation.Date?.Value.ToString() ?? "";
                            string commentText = annotation.Comment?.Value ?? "";

                            // Build a CSV line (escape commas in text).
                            string escapedText = commentText.Replace("\"", "\"\"");
                            if (escapedText.Contains(",") || escapedText.Contains("\""))
                            {
                                escapedText = $"\"{escapedText}\"";
                            }

                            string line = $"{Path.GetFileName(filePath)},{page.Name},{commentId},{reviewerId},{commentDate},{escapedText}";
                            reportLines.Add(line);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Report loading errors but continue processing other files.
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            // Write the consolidated report to a CSV file in the same folder.
            string reportPath = Path.Combine(folderPath, "VisioCommentsReport.csv");
            try
            {
                File.WriteAllLines(reportPath, reportLines);
                Console.WriteLine($"Report generated: {reportPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write report: {ex.Message}");
            }
        }
    }