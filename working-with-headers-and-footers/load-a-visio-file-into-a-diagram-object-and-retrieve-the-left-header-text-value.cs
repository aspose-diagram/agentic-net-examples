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
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found at path '{inputPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the left header text value from the diagram's HeaderFooter
            string leftHeader = diagram.HeaderFooter.HeaderLeft ?? string.Empty;

            // Output the retrieved header text
            Console.WriteLine($"Left Header Text: {leftHeader}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}