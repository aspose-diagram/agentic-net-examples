using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path of the .vsdx file
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a .vsdx file as a command‑line argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram into memory using a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Output basic information to confirm successful load
                Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");

                // Iterate through pages and display their IDs and names
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page ID: {page.ID}, Name: {page.NameU}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any loading errors to the error stream
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}