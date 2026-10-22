using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified VSDX file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the total number of pages in the diagram
            int pageCount = diagram.Pages.Count;

            // Output the result to the console
            Console.WriteLine($"Total number of pages: {pageCount}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}