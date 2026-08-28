using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // args[0] - input Visio file path
        // args[1] - start date (inclusive) in yyyy-MM-dd format
        // args[2] - end date (inclusive) in yyyy-MM-dd format
        // args[3] - output log file path
        if (args.Length < 4)
        {
            // Inform the user about missing arguments and exit gracefully
            Console.Error.WriteLine("Insufficient arguments. Usage: <inputVisio> <startDate> <endDate> <outputLog>");
            return;
        }

        string inputPath = args[0];
        string startDateStr = args[1];
        string endDateStr = args[2];
        string outputPath = args[3];

        // Guard to ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse start date; on failure, report and exit
        if (!DateTime.TryParse(startDateStr, out DateTime startDate))
        {
            Console.Error.WriteLine($"Invalid start date: {startDateStr}");
            return;
        }

        // Parse end date; on failure, report and exit
        if (!DateTime.TryParse(endDateStr, out DateTime endDate))
        {
            Console.Error.WriteLine($"Invalid end date: {endDateStr}");
            return;
        }

        // Load the diagram inside a try/catch to capture any Aspose errors
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        List<string> filteredComments = new List<string>();

        // Iterate through all pages and their annotations (comments)
        foreach (Page page in diagram.Pages)
        {
            // Annotations are stored in the PageSheet
            foreach (Annotation annotation in page.PageSheet.Annotations)
            {
                // Attempt to read the creation date if available via reflection
                DateTime? commentDate = null;
                try
                {
                    var dateObj = annotation.GetType().GetProperty("Date")?.GetValue(annotation);
                    if (dateObj != null)
                    {
                        var valueProp = dateObj.GetType().GetProperty("Value");
                        if (valueProp != null)
                        {
                            var val = valueProp.GetValue(dateObj);
                            if (val is DateTime dt)
                                commentDate = dt;
                            else if (val is string s && DateTime.TryParse(s, out DateTime parsed))
                                commentDate = parsed;
                        }
                        else if (dateObj is DateTime dtDirect)
                        {
                            commentDate = dtDirect;
                        }
                    }
                }
                catch
                {
                    // If reflection fails, ignore date filtering for this annotation
                }

                // Apply date range filter if a date was obtained
                bool withinRange = true;
                if (commentDate.HasValue)
                {
                    withinRange = commentDate.Value.Date >= startDate.Date && commentDate.Value.Date <= endDate.Date;
                }

                if (withinRange)
                {
                    // Retrieve comment text safely
                    string commentText = annotation.Comment?.Value ?? string.Empty;
                    // Retrieve reviewer ID (author) if present
                    int reviewerId = annotation.ReviewerID?.Value ?? -1;
                    // Shape ID the comment is attached to (0 if page comment)
                    long shapeId = annotation.ShapeID;
                    // Build a log line with all relevant information
                    string line = $"Page: {page.Name}, ShapeID: {shapeId}, ReviewerID: {reviewerId}, Date: {(commentDate.HasValue ? commentDate.Value.ToString("yyyy-MM-dd") : "N/A")}, Comment: {commentText}";
                    filteredComments.Add(line);
                }
            }
        }

        // Write filtered comments to the output log file; handle any I/O errors
        try
        {
            File.WriteAllLines(outputPath, filteredComments);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write output file: {ex.Message}");
        }
    }
}