using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (change as needed)
        string inputPath = "sample.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output HTML file path
        string outputHtmlPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "shape.html");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram does not contain any pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Ensure the page contains at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The selected page does not contain any shapes.");
                return;
            }

            // Get the first shape on the page
            Shape shape = page.Shapes[0];

            // Convert the shape to HTML preserving visual styling
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            shape.ToHTML(outputHtmlPath, htmlOptions);

            Console.WriteLine($"Shape successfully exported to HTML: {outputHtmlPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}