using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page of the diagram
            Page page = diagram.Pages[0];

            // Build the comment text and append the current timestamp
            string commentText = "Review needed. Created at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Add the comment at a specific location on the page (PinX = 1.0, PinY = 1.0)
            page.AddComment(1.0, 1.0, commentText);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
