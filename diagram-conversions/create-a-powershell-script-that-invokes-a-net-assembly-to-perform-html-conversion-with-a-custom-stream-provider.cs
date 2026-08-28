using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // Called before a resource stream is created
    public void InitStream(StreamProviderOptions options)
    {
        // Set a custom base URL for resources (e.g., images) referenced in the HTML
        options.CustomPath = "resources/";

        // Provide a stream for the resource; using a memory stream as a placeholder
        options.Stream = new MemoryStream();
    }

    // Called after the resource stream is no longer needed
    public void CloseStream(StreamProviderOptions options)
    {
        if (options.Stream != null)
        {
            options.Stream.Dispose();
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file (adjust the path as needed)
            string inputPath = "sample.vsdx";

            // Output HTML file
            string outputHtml = "output.html";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            var page = diagram.Pages[0];
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page
            long shapeId = page.Shapes[0].ID;
            Shape shape = page.Shapes.GetShape(shapeId);

            // Configure HTML save options with the custom stream provider
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                StreamProvider = new CustomStreamProvider()
            };

            // Export the shape to HTML
            shape.ToHTML(outputHtml, htmlOptions);

            Console.WriteLine($"Shape exported successfully to: {outputHtml}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}