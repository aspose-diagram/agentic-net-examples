using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, and reply text.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> <replyText>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string replyText = args[2];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Use the first page for this example.
            Page page = diagram.Pages[0];

            // Locate an existing comment to which we will add a reply.
            // Here we simply take the first comment found.
            Annotation targetComment = null;
            foreach (Annotation ann in page.PageSheet.Annotations)
            {
                targetComment = ann;
                break;
            }

            if (targetComment == null)
            {
                Console.Error.WriteLine("No existing comments found to reply to.");
                return;
            }

            // Retrieve the original comment's coordinates.
            double baseX = targetComment.X?.Value ?? 0.0;
            double baseY = targetComment.Y?.Value ?? 0.0;

            // Offset the reply slightly so it appears near the original comment.
            double replyX = baseX + 0.5;
            double replyY = baseY + 0.5;

            // Add the reply comment at the calculated position.
            page.AddComment(replyX, replyY, replyText);

            // Save the modified diagram. Using Vsdx as the output format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Reply added successfully. Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}