using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a file path argument was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio diagram file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram does not contain any pages.");
                return;
            }

            // Access the first page (index 0) – you can change the index to select a different page
            Page page = diagram.Pages[0];

            // Read the PageWidth property from the page's PageProps (value is in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;

            // Output the retrieved page width
            Console.WriteLine($"Page Width: {pageWidth} inches");
        }
        catch (Exception ex)
        {
            // Write any exception details to the error output
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}