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
            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (or you can retrieve by name)
            Page page = diagram.Pages[0];

            // Add a timestamped comment at position (1,1)
            AddTimestampedComment(page, 1.0, 1.0, "Review this diagram.");

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Adds a comment with the current system timestamp.
    static void AddTimestampedComment(Page page, double pinX, double pinY, string commentText)
    {
        // Current time in the system's time zone, formatted with offset
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss zzz");
        string fullComment = $"[{timestamp}] {commentText}";

        // Add the comment to the page at the specified coordinates
        page.AddComment(pinX, pinY, fullComment);
    }
}
