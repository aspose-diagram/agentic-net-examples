using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing Visio files
        string folderPath;
        if (args.Length > 0 && Directory.Exists(args[0]))
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the full path to the folder containing Visio files: ");
            folderPath = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!Directory.Exists(folderPath))
            {
                Console.Error.WriteLine("Folder does not exist. Exiting.");
                return;
            }
        }

        // Prepare the output CSV file
        string reportPath = Path.Combine(folderPath, "CommentReport.csv");
        using (var writer = new StreamWriter(reportPath, false))
        {
            // Write CSV header
            writer.WriteLine("FileName,PageName,CommentId,ReviewerId,CommentDate,CommentText");

            // Process each Visio file in the folder (common extensions)
            string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                // Guard: ensure the file actually exists before attempting to load
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"File not found: {filePath}");
                    continue;
                }

                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                    continue; // skip non‑Visio files

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through annotations (comments) on the page
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            // Extract required fields
                            long commentId = annotation.MarkerIndex.Value;
                            int reviewerId = annotation.ReviewerID.Value;

                            // Date is a struct; avoid null‑conditional on the struct itself
                            string commentDate = annotation.Date != null ? annotation.Date.Value.ToString() : string.Empty;

                            string commentText = annotation.Comment?.Value ?? string.Empty;

                            // Write a line to the CSV
                            string line = string.Format(
                                "\"{0}\",\"{1}\",{2},{3},\"{4}\",\"{5}\"",
                                Path.GetFileName(filePath),
                                page.Name ?? string.Empty,
                                commentId,
                                reviewerId,
                                commentDate,
                                commentText.Replace("\"", "\"\"") // escape quotes
                            );
                            writer.WriteLine(line);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Report processing errors to the error stream
                    Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }
        }

        Console.WriteLine($"Comment extraction completed. Report saved to: {reportPath}");
    }
}