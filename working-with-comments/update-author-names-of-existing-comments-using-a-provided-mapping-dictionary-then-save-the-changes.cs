using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Mapping of old author names to new author names
            var authorMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "John Doe", "Jane Smith" },
                { "Alice", "Bob" }
            };

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Access comments (annotations) via the page's PageSheet
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Retrieve the current comment text
                    string commentText = annotation.Comment.Value;

                    // Assume the author name appears before the first colon, e.g., "John Doe: Comment text"
                    int colonPos = commentText.IndexOf(':');
                    if (colonPos > 0)
                    {
                        string currentAuthor = commentText.Substring(0, colonPos).Trim();

                        // If the author exists in the mapping, replace it with the new name
                        if (authorMapping.TryGetValue(currentAuthor, out string newAuthor))
                        {
                            string updatedComment = newAuthor + commentText.Substring(colonPos);
                            annotation.Comment.Value = updatedComment;
                        }
                    }
                }
            }

            // Save the modified diagram back to a file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
