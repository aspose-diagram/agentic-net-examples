using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Retrieve the associated PageSheet
                    PageSheet pageSheet = page.PageSheet;

                    // Example configuration retrieval: page name and dimensions
                    string pageName = page.Name;
                    double pageWidth = pageSheet.PageProps.PageWidth.Value;
                    double pageHeight = pageSheet.PageProps.PageHeight.Value;

                    Console.WriteLine($"Page ID: {page.ID}, Name: {pageName}");
                    Console.WriteLine($"  Width: {pageWidth}, Height: {pageHeight}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}