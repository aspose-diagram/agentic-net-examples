using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the diagram file path (first argument or default)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Output page name (or placeholder if null)
                Console.WriteLine($"Page: {page.Name ?? "Unnamed"}");

                // First pass: collect all comment (annotation) IDs on this page
                List<int> commentIds = new List<int>();
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // MarkerIndex uniquely identifies a comment
                    int? id = annotation.MarkerIndex?.Value;
                    if (id.HasValue)
                    {
                        commentIds.Add(id.Value);
                    }
                }

                Console.WriteLine($"Found {commentIds.Count} comment(s) on this page.");

                // Second pass: retrieve detailed comment objects using the collected IDs
                foreach (int commentId in commentIds)
                {
                    Annotation foundAnnotation = null;

                    // Search for the annotation with the matching MarkerIndex
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        if (annotation.MarkerIndex?.Value == commentId)
                        {
                            foundAnnotation = annotation;
                            break;
                        }
                    }

                    if (foundAnnotation != null)
                    {
                        // Extract detailed information from the annotation
                        string commentText = foundAnnotation.Comment?.Value ?? string.Empty;
                        int reviewerId = foundAnnotation.ReviewerID?.Value ?? 0;
                        DateTime commentDate = foundAnnotation.Date?.Value ?? DateTime.MinValue;

                        // Output the detailed comment information
                        Console.WriteLine($"Comment ID: {commentId}");
                        Console.WriteLine($"  Reviewer ID: {reviewerId}");
                        Console.WriteLine($"  Date: {commentDate}");
                        Console.WriteLine($"  Text: {commentText}");
                    }
                    else
                    {
                        // This should not happen, but handle gracefully
                        Console.WriteLine($"Comment ID {commentId} could not be retrieved.");
                    }
                }

                Console.WriteLine(); // Blank line between pages
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}