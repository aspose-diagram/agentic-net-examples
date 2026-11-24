using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define the target page name to retrieve.
                string targetPageName = "Page-2";

                // Retrieve the page by its name.
                Page page = diagram.Pages.GetPage(targetPageName);

                if (page == null)
                {
                    Console.Error.WriteLine($"Page named '{targetPageName}' was not found in the diagram.");
                }
                else
                {
                    // Output basic page information.
                    Console.WriteLine($"Page Name: {page.NameU}");
                    Console.WriteLine($"Page ID: {page.ID}");

                    // Indicate whether the page is a background page.
                    if (page.Background == BOOL.True)
                    {
                        Console.WriteLine("Page type: Background");
                    }
                    else
                    {
                        Console.WriteLine("Page type: Foreground");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}