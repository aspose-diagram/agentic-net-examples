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

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the custom tag to prepend
            const string tag = "[Review] ";

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Access the annotations (comments) collection on the page sheet
                var annotations = page.PageSheet.Annotations;

                // Process each annotation
                foreach (Annotation annotation in annotations)
                {
                    // Retrieve the current comment text
                    string commentText = annotation.Comment.Value;

                    // Apply criteria: comment contains the word "TODO"
                    if (!string.IsNullOrEmpty(commentText) && commentText.Contains("TODO"))
                    {
                        // Add the custom tag if it's not already present
                        if (!commentText.StartsWith(tag))
                        {
                            annotation.Comment.Value = tag + commentText;
                        }
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
