using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string visioPath = "input.vsdx";
                // Output HTML file path
                string htmlPath = "comments.html";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // StringBuilder for constructing HTML content
                StringBuilder html = new StringBuilder();

                // Basic HTML structure with simple CSS and JavaScript for collapsible sections
                html.AppendLine("<!DOCTYPE html>");
                html.AppendLine("<html lang=\"en\">");
                html.AppendLine("<head>");
                html.AppendLine("    <meta charset=\"UTF-8\">");
                html.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                html.AppendLine("    <title>Visio Comments Export</title>");
                html.AppendLine("    <style>");
                html.AppendLine("        .collapsible {");
                html.AppendLine("            background-color: #f1f1f1;");
                html.AppendLine("            color: #444;");
                html.AppendLine("            cursor: pointer;");
                html.AppendLine("            padding: 10px;");
                html.AppendLine("            width: 100%;");
                html.AppendLine("            border: none;");
                html.AppendLine("            text-align: left;");
                html.AppendLine("            outline: none;");
                html.AppendLine("            font-size: 16px;");
                html.AppendLine("        }");
                html.AppendLine("        .content {");
                html.AppendLine("            padding: 0 10px;");
                html.AppendLine("            display: none;");
                html.AppendLine("            overflow: hidden;");
                html.AppendLine("            background-color: #fafafa;");
                html.AppendLine("        }");
                html.AppendLine("    </style>");
                html.AppendLine("    <script>");
                html.AppendLine("        function toggleContent(id) {");
                html.AppendLine("            var content = document.getElementById(id);");
                html.AppendLine("            if (content.style.display === \"block\") {");
                html.AppendLine("                content.style.display = \"none\";");
                html.AppendLine("            } else {");
                html.AppendLine("                content.style.display = \"block\";");
                html.AppendLine("            }");
                html.AppendLine("        }");
                html.AppendLine("    </script>");
                html.AppendLine("</head>");
                html.AppendLine("<body>");
                html.AppendLine("    <h1>Visio Diagram Comments</h1>");

                int commentCounter = 0;

                // Iterate through all pages and their annotations (comments)
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    var page = diagram.Pages[pageIndex];
                    foreach (Annotation comment in page.PageSheet.Annotations)
                    {
                        commentCounter++;
                        string sectionId = $"content{commentCounter}";
                        long markerId = comment.MarkerIndex.Value;
                        string commentText = comment.Comment.Value;
                        int reviewerId = comment.ReviewerID.Value;
                        int shapeId = comment.ShapeID; // Primitive int, no .Value needed

                        // Build collapsible section header with metadata
                        html.AppendLine($"    <button class=\"collapsible\" onclick=\"toggleContent('{sectionId}')\">");
                        html.AppendLine($"        Comment #{commentCounter} (Marker ID: {markerId})");
                        html.AppendLine("    </button>");
                        html.AppendLine($"    <div class=\"content\" id=\"{sectionId}\">");
                        html.AppendLine("        <ul>");
                        html.AppendLine($"            <li><strong>Page:</strong> {pageIndex + 1}</li>");
                        html.AppendLine($"            <li><strong>Shape ID:</strong> {shapeId}</li>");
                        html.AppendLine($"            <li><strong>Reviewer ID:</strong> {reviewerId}</li>");
                        html.AppendLine($"            <li><strong>Comment Text:</strong> {System.Net.WebUtility.HtmlEncode(commentText)}</li>");
                        html.AppendLine("        </ul>");
                        html.AppendLine("    </div>");
                    }
                }

                if (commentCounter == 0)
                {
                    html.AppendLine("    <p>No comments found in the diagram.</p>");
                }

                html.AppendLine("</body>");
                html.AppendLine("</html>");

                // Write the HTML content to the output file
                try
                {
                    File.WriteAllText(htmlPath, html.ToString(), Encoding.UTF8);
                    Console.WriteLine($"Successfully exported {commentCounter} comment(s) to '{htmlPath}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing HTML file: {ex.Message}");
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }