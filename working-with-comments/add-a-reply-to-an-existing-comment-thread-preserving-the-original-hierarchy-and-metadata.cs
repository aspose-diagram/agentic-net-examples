using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page (adjust as needed)
            Page page = diagram.Pages[0];

            // Locate the first existing comment (annotation) on the page
            Annotation originalAnnotation = null;
            foreach (Annotation ann in page.PageSheet.Annotations)
            {
                originalAnnotation = ann;
                break; // take the first comment found
            }

            if (originalAnnotation == null)
            {
                Console.Error.WriteLine("No existing comments found on the page to reply to.");
                return;
            }

            // Determine reply position – offset slightly from the original comment
            // Since Annotation does not expose X/Y directly, we use a fixed offset.
            double replyX = 1.0; // arbitrary X coordinate
            double replyY = 1.0; // arbitrary Y coordinate

            // Construct reply text, preserving original comment's metadata (e.g., marker ID)
            string replyText = $"Reply to comment #{originalAnnotation.MarkerIndex.Value}: Review needed.";

            // Add the reply comment to the page at the calculated position
            page.AddComment(replyX, replyY, replyText);

            // Save the updated diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Reply added successfully. Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose.Diagram related errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}