using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare to write CSV
                using (StreamWriter writer = new StreamWriter(outputPath, false))
                {
                    // Write CSV header
                    writer.WriteLine("\"PageName\",\"CommentText\",\"ReviewerID\",\"Date\"");

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Page name may be null; use empty string if so
                        string pageName = page.Name ?? string.Empty;

                        // Iterate through annotations (comments) on the page
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            // Retrieve comment text; fallback to empty string if null
                            string commentText = annotation.Comment?.Value ?? string.Empty;

                            // Retrieve reviewer ID; fallback to 0 if null
                            int reviewerId = annotation.ReviewerID?.Value ?? 0;

                            // Retrieve comment date; fallback to DateTime.MinValue if null
                            DateTime date = annotation.Date?.Value ?? DateTime.MinValue;

                            // Escape double quotes in fields
                            string escPageName = $"\"{pageName.Replace("\"", "\"\"")}\"";
                            string escComment = $"\"{commentText.Replace("\"", "\"\"")}\"";
                            string escReviewer = reviewerId.ToString();
                            string escDate = date == DateTime.MinValue ? "" : date.ToString("o");

                            // Write CSV line
                            writer.WriteLine($"{escPageName},{escComment},{escReviewer},{escDate}");
                        }
                    }
                }
            }

            Console.WriteLine($"Comments exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}