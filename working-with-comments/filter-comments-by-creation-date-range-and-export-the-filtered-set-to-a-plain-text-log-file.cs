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

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output plain‑text log file path
                string outputPath = "FilteredComments.log";

                // Define the date range for filtering (inclusive)
                DateTime startDate = new DateTime(2023, 1, 1);
                DateTime endDate   = new DateTime(2023, 12, 31);

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Collect filtered comment lines
                List<string> filteredLines = new List<string>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Access annotations (comments) on the page sheet
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve the comment creation date
                        // The Date property is read‑only; its value is accessed via .Value
                        DateTime commentDate = annotation.Date.Value;

                        // Check if the comment falls within the specified range
                        if (commentDate >= startDate && commentDate <= endDate)
                        {
                            // Retrieve comment text
                            string commentText = annotation.Comment.Value;

                            // Build a log line with page name, shape ID (if any), date, and text
                            string line = $"Page: {page.Name}, ShapeID: {annotation.ShapeID}, Date: {commentDate:yyyy-MM-dd}, Comment: {commentText}";
                            filteredLines.Add(line);
                        }
                    }
                }

                // Write the filtered comments to the plain‑text log file
                try
                {
                    using (StreamWriter writer = new StreamWriter(outputPath, false))
                    {
                        foreach (string line in filteredLines)
                        {
                            writer.WriteLine(line);
                        }
                    }

                    Console.WriteLine($"Filtered comments have been exported to '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    // Report any I/O errors
                    Console.WriteLine($"Error writing to log file: {ex.Message}");
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }