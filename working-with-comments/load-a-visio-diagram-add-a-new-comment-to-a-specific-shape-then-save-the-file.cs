using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class AddCommentExample
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputFile = "input.vsdx";
            string outputFile = "output.vsdx";

            // Load the Visio diagram from the file
            Diagram diagram = new Diagram(inputFile);

            // Get the active page (the page where the shape resides)
            Page page = diagram.ActivePage;

            // Identifier of the shape to which the comment will be added
            long shapeId = 1; // replace with the actual shape ID

            // The comment text
            string comment = "This is a comment added via Aspose.Diagram";

            // Add the comment to the specified shape
            page.AddComment(shapeId, comment);

            // Save the modified diagram back to a file (preserving the original format)
            diagram.Save(outputFile, SaveFileFormat.Vsdx);

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
