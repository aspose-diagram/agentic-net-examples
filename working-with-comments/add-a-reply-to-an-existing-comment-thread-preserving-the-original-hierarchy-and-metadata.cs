using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the target page (first page in this example)
            var page = diagram.Pages[0];

            // Retrieve a shape to which the reply will be attached.
            // Here we use the first shape on the page; adjust as needed.
            var shape = page.Shapes[0];

            // Add a reply comment to the selected shape.
            // This creates a new annotation linked to the same shape,
            // preserving the comment hierarchy (reply to the existing comment).
            string replyText = "This is a reply to the existing comment.";
            page.AddComment(shape, replyText);

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
