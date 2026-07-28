using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio diagram file
                string diagramPath = "input.vsdx";

                // Output plain‑text log file
                string logPath = "FilteredComments.log";

                // Define the date range for filtering comments
                DateTime startDate = new DateTime(2023, 1, 1);
                DateTime endDate   = new DateTime(2023, 12, 31);

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Open the log file for writing
                using (StreamWriter writer = new StreamWriter(logPath))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all annotations (comments) on the page
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            // Retrieve the creation date of the comment
                            // The Date property is a DateTimeValue; use .Value to get DateTime
                            DateTime commentDate = annotation.Date.Value;

                            // Check if the comment falls within the specified range
                            if (commentDate >= startDate && commentDate <= endDate)
                            {
                                // Gather comment details
                                long commentId   = annotation.MarkerIndex.Value;
                                long reviewerId  = annotation.ReviewerID.Value;
                                string commentText = annotation.Comment.Value;

                                // Write a formatted line to the log file
                                writer.WriteLine(
                                    $"Page: {page.Name}, CommentID: {commentId}, ReviewerID: {reviewerId}, Date: {commentDate:u}, Text: {commentText}");
                            }
                        }
                    }
                }

                Console.WriteLine($"Filtered comments have been exported to '{logPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }