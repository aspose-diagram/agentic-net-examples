using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: source diagram path (legacy) and output path (latest format)
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: MigrationTool <sourcePath> <outputPath>");
            return;
        }

        string sourcePath = args[0];
        string outputPath = args[1];

        // Load the legacy diagram
        Diagram sourceDiagram = new Diagram(sourcePath);

        // Iterate through all pages and list existing comments (annotations)
        foreach (Page page in sourceDiagram.Pages)
        {
            // Access annotations via the PageSheet
            foreach (Annotation annotation in page.PageSheet.Annotations)
            {
                // Retrieve comment text using the .Value property
                string commentText = annotation.Comment.Value;
                long commentId = annotation.MarkerIndex.Value;
                Console.WriteLine($"Page \"{page.Name}\" - Comment ID {commentId}: {commentText}");
            }
        }

        // Save the diagram in the latest Visio format (VSDX). Comments are preserved automatically.
        sourceDiagram.Save(outputPath, SaveFileFormat.Vsdx);

        Console.WriteLine($"Diagram migrated and saved to \"{outputPath}\".");
    }
}
