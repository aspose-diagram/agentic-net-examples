using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure a file path argument is provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a VDX file as a command-line argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the file exists before attempting to load
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram using the Diagram constructor inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the number of pages in the diagram
                int pageCount = diagram.Pages.Count;

                // Output the page count to the console
                Console.WriteLine($"Page count: {pageCount}");

                // Validate that the diagram contains at least one page
                if (pageCount <= 0)
                {
                    throw new Exception("The diagram contains no pages.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors encountered during loading or validation to standard error
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}