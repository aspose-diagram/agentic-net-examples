using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the input Visio file (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the guidelines
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the page named "Page-2"
                Page page = diagram.Pages.GetPage("Page-2");

                // Check if the page was found
                if (page == null)
                {
                    Console.Error.WriteLine("Error: Page named \"Page-2\" was not found in the diagram.");
                    return;
                }

                // Output basic information about the retrieved page
                Console.WriteLine($"Page Name: {page.NameU}");
                Console.WriteLine($"Page ID: {page.ID}");
                Console.WriteLine($"Is Background Page: {(page.Background == BOOL.True ? "Yes" : "No")}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}