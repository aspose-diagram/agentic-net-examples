using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string inputPath = "input.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Print markdown table header
                Console.WriteLine("| Shape ID | Comment |");
                Console.WriteLine("|---|---|");

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Annotations (comments) are stored in the page's PageSheet
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // ShapeID is an integer identifying the shape the comment is attached to
                        int shapeId = annotation.ShapeID;

                        // Comment text is stored in the Comment cell; use .Value to retrieve the string
                        string commentText = annotation.Comment?.Value ?? string.Empty;

                        // Escape pipe characters to keep markdown table integrity
                        commentText = commentText.Replace("|", "\\|");

                        // Output a markdown table row
                        Console.WriteLine($"| {shapeId} | {commentText} |");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }