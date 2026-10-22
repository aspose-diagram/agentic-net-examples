using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a diagram file path was provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No diagram file path specified.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages and log their width and height.
            int pageIndex = 0;
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (values are in inches).
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;

                // Log the dimensions using a formatted string.
                Console.WriteLine($"Page {pageIndex + 1}: Width = {width} inches, Height = {height} inches");

                pageIndex++;
            }

            // Dispose the diagram to release resources.
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}