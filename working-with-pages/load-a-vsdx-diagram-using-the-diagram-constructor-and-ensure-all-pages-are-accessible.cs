using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that a file path argument was provided
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input_vsdx_path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the specified file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve and display the total number of pages
                int pageCount = diagram.Pages.Count;
                Console.WriteLine($"Diagram loaded successfully. Total pages: {pageCount}");

                // Iterate over each page to confirm accessibility
                for (int i = 0; i < pageCount; i++)
                {
                    // Access the page by index
                    Aspose.Diagram.Page page = diagram.Pages[i];

                    // Extract page details (ID and name)
                    long pageId = page.ID;
                    string pageName = page.NameU ?? "(no name)";

                    // Output page information
                    Console.WriteLine($"Page {i + 1}: ID = {pageId}, Name = {pageName}");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}