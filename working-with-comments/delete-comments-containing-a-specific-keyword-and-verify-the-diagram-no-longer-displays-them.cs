using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path where the modified file will be saved
            string outputPath = "output.vsdx";
            // Keyword to search for inside comments
            string keyword = "TODO";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Access the collection of annotations (comments) on the page
                var annotations = page.PageSheet.Annotations;

                // Remove annotations that contain the keyword.
                // Iterate backwards to safely remove items while looping.
                for (int i = annotations.Count - 1; i >= 0; i--)
                {
                    Annotation ann = annotations[i];
                    string commentText = ann.Comment.Value ?? string.Empty;

                    if (commentText.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        // Delete the annotation
                        annotations.RemoveAt(i);
                    }
                }
            }

            // Verification: ensure no remaining comment contains the keyword
            foreach (Page page in diagram.Pages)
            {
                foreach (Annotation ann in page.PageSheet.Annotations)
                {
                    string commentText = ann.Comment.Value ?? string.Empty;
                    if (commentText.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception("Keyword still found in a comment after deletion.");
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Comments containing the keyword have been removed and the diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
