using System;
using System.IO;
using System.Net;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputHtmlFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare the HTML output file
                using (StreamWriter writer = new StreamWriter(outputPath, false))
                {
                    // Write basic HTML structure
                    writer.WriteLine("<!DOCTYPE html>");
                    writer.WriteLine("<html lang=\"en\">");
                    writer.WriteLine("<head>");
                    writer.WriteLine("    <meta charset=\"UTF-8\">");
                    writer.WriteLine("    <title>Visio Comments Export</title>");
                    writer.WriteLine("    <style>");
                    writer.WriteLine("        details { margin-bottom: 1em; }");
                    writer.WriteLine("        summary { font-weight: bold; cursor: pointer; }");
                    writer.WriteLine("        .metadata { font-size: 0.9em; color: #555; margin-top: 0.5em; }");
                    writer.WriteLine("        .comment { margin-top: 0.5em; }");
                    writer.WriteLine("    </style>");
                    writer.WriteLine("</head>");
                    writer.WriteLine("<body>");
                    writer.WriteLine("    <h1>Visio Comments Export</h1>");

                    // Iterate through each page and its annotations (comments)
                    foreach (Page page in diagram.Pages)
                    {
                        // Determine whether the page contains any comments
                        bool pageHasComments = false;
                        foreach (Annotation ann in page.PageSheet.Annotations)
                        {
                            pageHasComments = true;
                            break;
                        }

                        if (!pageHasComments)
                            continue; // Skip pages without comments

                        // Write page header (page.Name is a plain string)
                        writer.WriteLine($"    <h2>Page: {page.Name ?? "Unnamed"}</h2>");

                        // Process each annotation on the page
                        foreach (Annotation ann in page.PageSheet.Annotations)
                        {
                            // Retrieve comment metadata
                            int markerId = ann.MarkerIndex.Value;
                            int reviewerId = ann.ReviewerID.Value;
                            string author = $"Reviewer {reviewerId}";
                            string commentText = ann.Comment?.Value ?? string.Empty;
                            DateTime commentDate = ann.Date?.Value ?? DateTime.MinValue;
                            int shapeId = ann.ShapeID; // Primitive int, no .Value needed

                            // Escape HTML special characters in comment text
                            string escapedComment = WebUtility.HtmlEncode(commentText);

                            // Write a collapsible section for the comment
                            writer.WriteLine("    <details>");
                            writer.WriteLine($"        <summary>Comment #{markerId} by {author} on {commentDate:G}</summary>");
                            writer.WriteLine("        <div class=\"metadata\">");
                            writer.WriteLine($"            <p>Shape ID: {shapeId}</p>");
                            writer.WriteLine($"            <p>Reviewer ID: {reviewerId}</p>");
                            writer.WriteLine("        </div>");
                            writer.WriteLine("        <div class=\"comment\">");
                            writer.WriteLine($"            <p>{escapedComment}</p>");
                            writer.WriteLine("        </div>");
                            writer.WriteLine("    </details>");
                        }
                    }

                    // Close HTML document
                    writer.WriteLine("</body>");
                    writer.WriteLine("</html>");
                }

                Console.WriteLine($"Comments exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}