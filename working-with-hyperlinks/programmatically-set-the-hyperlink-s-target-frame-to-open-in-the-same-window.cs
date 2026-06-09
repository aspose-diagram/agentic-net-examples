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

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                throw new Exception("The diagram contains no pages.");
            }

            Page page = diagram.Pages[0];

            if (page.Shapes.Count == 0)
            {
                throw new Exception("The first page contains no shapes.");
            }

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // Create a new hyperlink
            Hyperlink link = new Hyperlink
            {
                Name = "MyLink"
            };
            // Set the target address
            link.Address.Value = "https://example.com";

            // Set the hyperlink to open in the same window (NewWindow = False)
            link.NewWindow.Value = BOOL.False;

            // Ensure the frame target is empty (default behavior)
            link.Frame.Value = "";

            // Add the hyperlink to the shape's Hyperlinks collection
            shape.Hyperlinks.Add(link);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
