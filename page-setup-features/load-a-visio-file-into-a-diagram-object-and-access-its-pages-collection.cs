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
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the specified Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(visioPath);

            // Access the Pages collection and output basic information for each page
            Console.WriteLine($"Diagram contains {diagram.Pages.Count} page(s).");
            foreach (Page page in diagram.Pages)
            {
                // Page.ID is an integer identifier, Page.Name is the page's name
                Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}