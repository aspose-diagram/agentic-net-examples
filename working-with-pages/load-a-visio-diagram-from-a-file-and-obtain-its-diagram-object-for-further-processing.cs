using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input file path (argument or prompt)
        string inputPath;
        if (args.Length > 0)
        {
            inputPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the Visio file: ");
            inputPath = Console.ReadLine();
        }

        // Guard: ensure the file exists
        if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Example access: output the number of pages
                Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");
                // The 'diagram' object is now available for further processing
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}