using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (replace with your actual file path)
                string visioPath = "input.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Prepare markdown table header
                Console.WriteLine("| Shape ID | Comment |");
                Console.WriteLine("|----------|---------|");

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Annotations (comments) are stored at the page level
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // ShapeID is an integer; convert to long for consistency
                        long shapeId = annotation.ShapeID;

                        // Retrieve the comment text
                        string commentText = annotation.Comment.Value ?? string.Empty;

                        // Output a markdown table row
                        Console.WriteLine($"| {shapeId} | {EscapeMarkdown(commentText)} |");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Escape pipe characters to keep markdown table integrity
        private static string EscapeMarkdown(string text)
        {
            return text.Replace("|", "\\|");
        }
    }