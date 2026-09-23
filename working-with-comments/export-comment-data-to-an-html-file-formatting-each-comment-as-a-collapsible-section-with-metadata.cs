using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Path for the generated HTML file
        string outputPath = "CommentsExport.html";

        // StringBuilder for constructing HTML content
        StringBuilder htmlBuilder = new StringBuilder();

        // HTML header
        htmlBuilder.AppendLine("<!DOCTYPE html>");
        htmlBuilder.AppendLine("<html>");
        htmlBuilder.AppendLine("<head>");
        htmlBuilder.AppendLine("<meta charset=\"utf-8\" />");
        htmlBuilder.AppendLine("<title>Visio Comments Export</title>");
        htmlBuilder.AppendLine("<style>");
        htmlBuilder.AppendLine("details { margin-bottom: 10px; }");
        htmlBuilder.AppendLine("summary { font-weight: bold; cursor: pointer; }");
        htmlBuilder.AppendLine("</style>");
        htmlBuilder.AppendLine("</head>");
        htmlBuilder.AppendLine("<body>");
        htmlBuilder.AppendLine("<h1>Visio Comments</h1>");

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page using an index‑based loop (Page.Index does not exist)
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Page name may be null; fallback to page number (1‑based)
                string pageName = !string.IsNullOrEmpty(page.Name) ? page.Name : $"Page {i + 1}";

                // Iterate over annotations (comments) on the current page
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Retrieve comment metadata
                    long markerId = annotation.MarkerIndex.Value;
                    string commentText = annotation.Comment.Value;
                    int reviewerId = annotation.ReviewerID.Value;
                    int shapeId = annotation.ShapeID; // primitive int

                    // Build a collapsible section for each comment
                    htmlBuilder.AppendLine("<details>");
                    htmlBuilder.AppendLine($"<summary>Comment {markerId} (Reviewer {reviewerId})</summary>");
                    htmlBuilder.AppendLine("<div>");
                    htmlBuilder.AppendLine($"<p><strong>Page:</strong> {pageName}</p>");
                    htmlBuilder.AppendLine($"<p><strong>Shape ID:</strong> {shapeId}</p>");
                    htmlBuilder.AppendLine($"<p>{System.Net.WebUtility.HtmlEncode(commentText)}</p>");
                    htmlBuilder.AppendLine("</div>");
                    htmlBuilder.AppendLine("</details>");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // HTML footer
        htmlBuilder.AppendLine("</body>");
        htmlBuilder.AppendLine("</html>");

        // Write the HTML content to the output file
        File.WriteAllText(outputPath, htmlBuilder.ToString(), Encoding.UTF8);

        Console.WriteLine($"Comments have been exported to '{outputPath}'.");
    }
}