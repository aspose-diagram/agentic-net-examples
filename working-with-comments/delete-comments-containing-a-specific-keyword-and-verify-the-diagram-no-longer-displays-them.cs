using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output Visio file path after comment removal
            string outputPath = "output.vsdx";
            // Keyword to search for in comments
            string keyword = "TODO";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and remove comments containing the keyword
            foreach (Page page in diagram.Pages)
            {
                // Annotations (comments) are stored in the page's PageSheet
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    string commentText = annotation.Comment.Value;
                    if (!string.IsNullOrEmpty(commentText) && commentText.Contains(keyword))
                    {
                        // Clear the comment text to effectively delete it
                        annotation.Comment.Value = string.Empty;
                    }
                }
            }

            // Verification: ensure no remaining comment contains the keyword
            foreach (Page page in diagram.Pages)
            {
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    string commentText = annotation.Comment.Value;
                    if (!string.IsNullOrEmpty(commentText) && commentText.Contains(keyword))
                    {
                        throw new Exception($"Comment with keyword \"{keyword}\" still exists after deletion.");
                    }
                }
            }

            Console.WriteLine("All comments containing the keyword have been removed successfully.");

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
