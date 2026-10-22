using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputHtmlFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare HTML output.
                var html = new StringBuilder();
                html.AppendLine("<!DOCTYPE html>");
                html.AppendLine("<html lang=\"en\">");
                html.AppendLine("<head>");
                html.AppendLine("    <meta charset=\"UTF-8\">");
                html.AppendLine("    <title>Visio Comments Export</title>");
                html.AppendLine("    <style>");
                html.AppendLine("        details { margin-bottom: 1em; }");
                html.AppendLine("        summary { font-weight: bold; cursor: pointer; }");
                html.AppendLine("        .metadata { font-size: 0.9em; color: #555; }");
                html.AppendLine("    </style>");
                html.AppendLine("</head>");
                html.AppendLine("<body>");
                html.AppendLine("    <h1>Comments Export</h1>");

                // Iterate through each page and its annotations (comments).
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Annotations are stored on the page sheet.
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve comment text.
                        string commentText = annotation.Comment?.Value ?? string.Empty;

                        // Retrieve reviewer (author) ID.
                        int reviewerId = annotation.ReviewerID?.Value ?? 0;

                        // Retrieve creation date.
                        DateTime date = annotation.Date?.Value ?? DateTime.MinValue;
                        string dateString = date == DateTime.MinValue ? "N/A" : date.ToString("yyyy-MM-dd HH:mm:ss");

                        // Retrieve associated shape ID.
                        int shapeId = annotation.ShapeID;

                        // Build a collapsible section for this comment.
                        html.AppendLine("    <details>");
                        html.AppendLine($"        <summary>Comment on Page {pageIndex + 1} (ID: {annotation.MarkerIndex?.Value ?? 0})</summary>");
                        html.AppendLine("        <div class=\"metadata\">");
                        html.AppendLine($"            <p><strong>Author ID:</strong> {reviewerId}</p>");
                        html.AppendLine($"            <p><strong>Date:</strong> {dateString}</p>");
                        html.AppendLine($"            <p><strong>Shape ID:</strong> {shapeId}</p>");
                        html.AppendLine("        </div>");
                        html.AppendLine("        <p>" + System.Net.WebUtility.HtmlEncode(commentText) + "</p>");
                        html.AppendLine("    </details>");
                    }
                }

                html.AppendLine("</body>");
                html.AppendLine("</html>");

                // Write the HTML content to the output file.
                File.WriteAllText(outputPath, html.ToString(), Encoding.UTF8);
                Console.WriteLine($"Comments exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}