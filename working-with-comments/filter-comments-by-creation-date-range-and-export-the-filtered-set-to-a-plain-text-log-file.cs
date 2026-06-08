using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output plain‑text log file path
                string outputLogPath = "filtered_comments.txt";

                // Define the date range for filtering comments
                DateTime startDate = new DateTime(2023, 1, 1);
                DateTime endDate   = new DateTime(2023, 12, 31);

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare the log file
                using (StreamWriter writer = new StreamWriter(outputLogPath, false))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Access annotations (comments) via the PageSheet
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            // Attempt to read the creation date.
                            // The Annotation class provides a Date property (DateTimeValue) in recent versions.
                            // If the property is not present, this line will cause a compile‑time error,
                            // indicating that the used Aspose.Diagram version does not support date filtering.
                            DateTime commentDate = annotation.Date?.Value ?? DateTime.MinValue;

                            // Filter by the specified date range
                            if (commentDate >= startDate && commentDate <= endDate)
                            {
                                // Retrieve comment details
                                long commentId = annotation.MarkerIndex.Value;
                                int reviewerId = annotation.ReviewerID.Value;
                                string commentText = annotation.Comment?.Value ?? string.Empty;

                                // Write a formatted line to the log file
                                writer.WriteLine($"Page: {page.Name}, CommentID: {commentId}, ReviewerID: {reviewerId}, Date: {commentDate:yyyy-MM-dd}, Text: {commentText}");
                            }
                        }
                    }
                }

                // Optional: inform the user
                Console.WriteLine($"Filtered comments have been exported to '{outputLogPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }