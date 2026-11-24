using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all annotations (comments) on the current page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve the comment text; use empty string if null
                        string commentText = annotation.Comment?.Value ?? string.Empty;

                        // Check if the comment text is empty or whitespace
                        if (string.IsNullOrWhiteSpace(commentText))
                        {
                            // Report a warning with page name and marker ID
                            Console.WriteLine(
                                $"Warning: Empty comment found on page '{page.Name}' (Marker ID: {annotation.MarkerIndex?.Value ?? 0}).");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}