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

                // Input Visio file path (first argument or default)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Output HTML file path (second argument or default)
                string outputPath = args.Length > 1 ? args[1] : "comments.html";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    var sb = new StringBuilder();

                    // Basic HTML structure with simple styling
                    sb.AppendLine("<!DOCTYPE html>");
                    sb.AppendLine("<html lang=\"en\">");
                    sb.AppendLine("<head>");
                    sb.AppendLine("    <meta charset=\"UTF-8\">");
                    sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                    sb.AppendLine("    <title>Visio Comments Export</title>");
                    sb.AppendLine("    <style>");
                    sb.AppendLine("        body { font-family: Arial, sans-serif; margin: 20px; }");
                    sb.AppendLine("        details { margin-bottom: 10px; border: 1px solid #ccc; padding: 5px; }");
                    sb.AppendLine("        summary { font-weight: bold; cursor: pointer; }");
                    sb.AppendLine("        .metadata { margin-left: 20px; font-size: 0.9em; color: #555; }");
                    sb.AppendLine("    </style>");
                    sb.AppendLine("</head>");
                    sb.AppendLine("<body>");
                    sb.AppendLine("    <h1>Visio Comments</h1>");

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all annotations (comments) on the page
                        foreach (Annotation comment in page.PageSheet.Annotations)
                        {
                            // Retrieve comment data using .Value where required
                            long markerId = comment.MarkerIndex.Value;
                            string text = comment.Comment.Value ?? string.Empty;
                            int reviewerId = comment.ReviewerID.Value;
                            int shapeId = comment.ShapeID; // primitive int, no .Value needed

                            // Build a collapsible section for each comment
                            sb.AppendLine("    <details>");
                            sb.AppendLine($"        <summary>Comment #{markerId}</summary>");
                            sb.AppendLine("        <div class=\"metadata\">");
                            sb.AppendLine($"            <p><strong>Page:</strong> {page.Name}</p>");
                            sb.AppendLine($"            <p><strong>Shape ID:</strong> {shapeId}</p>");
                            sb.AppendLine($"            <p><strong>Reviewer ID:</strong> {reviewerId}</p>");
                            sb.AppendLine("        </div>");
                            sb.AppendLine($"        <p>{System.Net.WebUtility.HtmlEncode(text)}</p>");
                            sb.AppendLine("    </details>");
                        }
                    }

                    sb.AppendLine("</body>");
                    sb.AppendLine("</html>");

                    // Write the generated HTML to the output file
                    File.WriteAllText(outputPath, sb.ToString());

                    Console.WriteLine($"Comments exported successfully to '{outputPath}'.");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }