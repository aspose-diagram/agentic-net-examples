using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input file path from command-line arguments.
        string inputPath;
        if (args.Length > 0)
        {
            inputPath = args[0];
        }
        else
        {
            Console.Error.WriteLine("Please provide the path to a .vsdx file as a command-line argument.");
            return;
        }

        // Guard: ensure the specified file exists before attempting to load.
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
                // Diagram loaded successfully. Output basic information.
                Console.WriteLine($"Diagram loaded successfully. Total pages: {diagram.Pages.Count}");

                // Iterate through pages and display their names.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];
                    Console.WriteLine($"Page {i + 1}: NameU = {page.NameU}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any loading errors to the error output.
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}