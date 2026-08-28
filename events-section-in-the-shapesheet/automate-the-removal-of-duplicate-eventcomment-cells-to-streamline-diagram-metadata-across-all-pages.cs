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

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Keep track of comment texts that have already been encountered
                    HashSet<string> seenComments = new HashSet<string>();
                    // Collect annotations that are duplicates
                    List<Annotation> duplicates = new List<Annotation>();

                    // Iterate through all annotations (comments) on the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        string commentText = annotation.Comment.Value ?? string.Empty;

                        // If this comment text was seen before, mark it for removal
                        if (seenComments.Contains(commentText))
                        {
                            duplicates.Add(annotation);
                        }
                        else
                        {
                            seenComments.Add(commentText);
                        }
                    }

                    // Remove the duplicate annotations from the page
                    foreach (Annotation dup in duplicates)
                    {
                        page.PageSheet.Annotations.Remove(dup);
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Duplicate EventComment cells removed and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
