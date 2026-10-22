using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing Visio files.
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the folder containing Visio files: ");
            folderPath = Console.ReadLine();
        }

        // Validate folder existence.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("Folder does not exist: " + folderPath);
            return;
        }

        // Prepare the output report file.
        string reportPath = Path.Combine(folderPath, "CommentReport.csv");
        var reportLines = new List<string>();
        // Header line for CSV.
        reportLines.Add("FileName,PageName,CommentID,ReviewerID,CommentText,CommentDate");

        // Define supported Visio extensions.
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vstx", ".vsx", ".vtx", ".vss", ".vst" };

        // Iterate over each file in the folder.
        foreach (string filePath in Directory.GetFiles(folderPath))
        {
            // Process only files with supported extensions.
            if (Array.IndexOf(extensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                continue;

            // Guard against missing file (should not happen from GetFiles, but per requirements).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("File not found: " + filePath);
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Ensure the page has a PageSheet (always true for valid pages).
                    if (page.PageSheet == null)
                        continue;

                    // Iterate through each annotation (comment) on the page.
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Extract comment details.
                        string fileName = Path.GetFileName(filePath);
                        string pageName = page.Name;
                        int commentId = annotation.MarkerIndex.Value;
                        int reviewerId = annotation.ReviewerID.Value;
                        string commentText = annotation.Comment.Value?.Replace("\"", "\"\""); // Escape quotes for CSV.
                        DateTime commentDate = annotation.Date?.Value ?? DateTime.MinValue;

                        // Build CSV line (quote fields that may contain commas).
                        string csvLine = $"\"{fileName}\",\"{pageName}\",{commentId},{reviewerId},\"{commentText}\",\"{commentDate:O}\"";
                        reportLines.Add(csvLine);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur while processing a file.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }

        try
        {
            // Write all collected lines to the report file.
            File.WriteAllLines(reportPath, reportLines, Encoding.UTF8);
            Console.WriteLine("Comment report generated at: " + reportPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Failed to write report file: " + ex.Message);
        }
    }
}