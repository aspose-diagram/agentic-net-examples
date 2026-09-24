using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file. Can be passed as a command‑line argument or hard‑coded.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Print markdown table header.
                Console.WriteLine("| Shape ID | Comment |");
                Console.WriteLine("|---|---|");

                // Iterate through all pages and their annotations (comments).
                foreach (Page page in diagram.Pages)
                {
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // ShapeID is an integer that identifies the shape the comment is attached to.
                        int shapeId = annotation.ShapeID;

                        // Comment text is stored in the Comment cell; use .Value to retrieve the string.
                        string commentText = annotation.Comment.Value ?? string.Empty;

                        // Escape pipe characters to keep the markdown table valid.
                        commentText = commentText.Replace("|", "\\|");

                        Console.WriteLine($"| {shapeId} | {commentText} |");
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }