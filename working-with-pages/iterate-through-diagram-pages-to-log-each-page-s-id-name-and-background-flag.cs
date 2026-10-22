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
            Console.Error.WriteLine("Please provide the path to a Visio file as the first argument.");
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
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page properties
                    int pageId = page.ID;
                    string pageName = page.Name;
                    // Background flag is a BOOL; compare with BOOL.True
                    string isBackground = page.Background == BOOL.True ? "True" : "False";

                    // Log the page information
                    Console.WriteLine($"Page ID: {pageId}, Name: \"{pageName}\", IsBackground: {isBackground}");
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading or processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}