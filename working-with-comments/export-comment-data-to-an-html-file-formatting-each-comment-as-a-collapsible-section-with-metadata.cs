using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (replace with actual file path)
                string diagramPath = "input.vsdx";

                // Path where the generated HTML will be saved
                string htmlOutputPath = "comments.html";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Build the HTML content
                StringBuilder htmlBuilder = new StringBuilder();

                // Basic HTML header
                htmlBuilder.AppendLine("<!DOCTYPE html>");
                htmlBuilder.AppendLine("<html lang=\"en\">");
                htmlBuilder.AppendLine("<head>");
                htmlBuilder.AppendLine("    <meta charset=\"UTF-8\">");
                htmlBuilder.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                htmlBuilder.AppendLine("    <title>Diagram Comments</title>");
                // Simple style for better readability
                htmlBuilder.AppendLine("    <style>");
                htmlBuilder.AppendLine("        body { font-family: Arial, sans-serif; margin: 20px; }");
                htmlBuilder.AppendLine("        details { margin-bottom: 10px; }");
                htmlBuilder.AppendLine("        summary { font-weight: bold; cursor: pointer; }");
                htmlBuilder.AppendLine("        .metadata { margin-left: 20px; color: #555; }");
                htmlBuilder.AppendLine("        .comment-text { margin-left: 20px; white-space: pre-wrap; }");
                htmlBuilder.AppendLine("    </style>");
                htmlBuilder.AppendLine("</head>");
                htmlBuilder.AppendLine("<body>");
                htmlBuilder.AppendLine("    <h1>Diagram Comments</h1>");

                // Iterate through each page and its annotations (comments)
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];
                    // Ensure the page actually contains annotations
                    if (page.PageSheet.Annotations == null || page.PageSheet.Annotations.Count == 0)
                    {
                        continue;
                    }

                    htmlBuilder.AppendLine($"    <h2>Page {pageIndex + 1}: {page.Name}</h2>");

                    foreach (Annotation comment in page.PageSheet.Annotations)
                    {
                        // Retrieve comment data using the .Value property as required by the API
                        long markerId = comment.MarkerIndex.Value;
                        int reviewerId = comment.ReviewerID.Value;
                        int shapeId = comment.ShapeID; // ShapeID is a primitive int
                        string commentText = comment.Comment.Value ?? string.Empty;

                        // Create a collapsible section for each comment
                        htmlBuilder.AppendLine("    <details>");
                        htmlBuilder.AppendLine($"        <summary>Comment {markerId} (Reviewer {reviewerId})</summary>");
                        htmlBuilder.AppendLine("        <div class=\"metadata\">");
                        htmlBuilder.AppendLine($"            <p><strong>Marker ID:</strong> {markerId}</p>");
                        htmlBuilder.AppendLine($"            <p><strong>Reviewer ID:</strong> {reviewerId}</p>");
                        htmlBuilder.AppendLine($"            <p><strong>Shape ID:</strong> {shapeId}</p>");
                        htmlBuilder.AppendLine($"            <p><strong>Page Index:</strong> {pageIndex}</p>");
                        htmlBuilder.AppendLine("        </div>");
                        htmlBuilder.AppendLine("        <div class=\"comment-text\">");
                        htmlBuilder.AppendLine($"            {System.Net.WebUtility.HtmlEncode(commentText).Replace("\n", "<br/>")}");
                        htmlBuilder.AppendLine("        </div>");
                        htmlBuilder.AppendLine("    </details>");
                    }
                }

                // Close HTML tags
                htmlBuilder.AppendLine("</body>");
                htmlBuilder.AppendLine("</html>");

                // Write the HTML content to the output file
                File.WriteAllText(htmlOutputPath, htmlBuilder.ToString(), Encoding.UTF8);

                Console.WriteLine($"Comments have been exported to '{htmlOutputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }