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
            Console.Error.WriteLine("Please provide the path to a VSDX file as a command-line argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram using default load options inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Indicate successful loading and display basic information
                Console.WriteLine($"Successfully loaded diagram: {inputPath}");
                Console.WriteLine($"Number of pages: {diagram.Pages.Count}");
            }
        }
        catch (Exception ex)
        {
            // Write any loading errors to the error stream
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}