using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare markdown table header
            Console.WriteLine("| Shape ID | Comment |");
            Console.WriteLine("|----------|---------|");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Ensure the page has a PageSheet and Annotations collection
                if (page.PageSheet == null || page.PageSheet.Annotations == null)
                    continue;

                // Iterate through each annotation (comment) on the page
                foreach (Annotation ann in page.PageSheet.Annotations)
                {
                    // Retrieve the associated shape ID (convert to long for consistency)
                    long shapeId = ann.ShapeID;

                    // Retrieve the comment text; handle possible nulls
                    string comment = ann.Comment?.Value ?? string.Empty;

                    // Escape pipe characters in comment to preserve markdown table format
                    comment = comment.Replace("|", "\\|");

                    // Output the markdown row
                    Console.WriteLine($"| {shapeId} | {comment} |");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}