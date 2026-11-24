using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input file path (command‑line argument or user prompt)
        string inputPath;
        if (args.Length > 0)
        {
            inputPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the Visio file to load: ");
            inputPath = Console.ReadLine();
        }

        // Guard: ensure the file exists before proceeding
        if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: The file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Output basic information about the loaded diagram
                Console.WriteLine($"Diagram loaded successfully from \"{inputPath}\".");
                Console.WriteLine($"Number of pages: {diagram.Pages.Count}");

                // Iterate over pages and display their ID and Name
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Page.Name is a plain string; no .Value property is required
                    string pageName = page.Name ?? "(no name)";
                    Console.WriteLine($"Page ID: {page.ID}, Name: {pageName}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"An error occurred while loading the diagram: {ex.Message}");
        }
    }
}