using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the Visio file path: use first argument or prompt the user.
        string visioPath = args.Length > 0 ? args[0] : "";
        if (string.IsNullOrWhiteSpace(visioPath))
        {
            Console.Write("Enter the full path to the Visio file: ");
            visioPath = Console.ReadLine()?.Trim() ?? "";
        }

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the Visio file into a Diagram object.
            diagram = new Diagram(visioPath);
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading.
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Verify that the Diagram object was initialized and contains at least one page.
        if (diagram != null && diagram.Pages != null && diagram.Pages.Count > 0)
        {
            Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");
        }
        else
        {
            Console.Error.WriteLine("Diagram loaded but contains no pages.");
        }
    }
}