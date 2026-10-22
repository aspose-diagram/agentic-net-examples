using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect four arguments: input diagram path, start date, end date, output log path
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <startDate> <endDate> <outputLogPath>");
            return;
        }

        string inputPath = args[0];
        string startDateStr = args[1];
        string endDateStr = args[2];
        string outputPath = args[3];

        // Guard: verify input diagram file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse start and end dates
        if (!DateTime.TryParse(startDateStr, out DateTime startDate))
        {
            Console.Error.WriteLine($"Invalid start date: {startDateStr}");
            return;
        }

        if (!DateTime.TryParse(endDateStr, out DateTime endDate))
        {
            Console.Error.WriteLine($"Invalid end date: {endDateStr}");
            return;
        }

        // Ensure output directory exists
        try
        {
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to prepare output directory: {ex.Message}");
            return;
        }

        // Load diagram and filter comments
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                using (StreamWriter writer = new StreamWriter(outputPath, false))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all annotations (comments) on the page
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            // Retrieve the comment creation date; Annotation.Date is a DateValue
                            DateTime commentDate = annotation.Date.Value;

                            // Check if the comment falls within the specified date range (inclusive)
                            if (commentDate >= startDate && commentDate <= endDate)
                            {
                                // Retrieve comment text; Annotation.Comment is a Str2Value
                                string commentText = annotation.Comment?.Value ?? string.Empty;

                                // Retrieve reviewer ID; Annotation.ReviewerID is an IntValue
                                string reviewerId = annotation.ReviewerID?.Value.ToString() ?? string.Empty;

                                // Write a formatted line to the log file
                                writer.WriteLine($"[{commentDate:yyyy-MM-dd HH:mm:ss}] ReviewerID: {reviewerId} - {commentText}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Indicate successful completion
        Console.WriteLine($"Filtered comments have been exported to: {outputPath}");
    }
}
