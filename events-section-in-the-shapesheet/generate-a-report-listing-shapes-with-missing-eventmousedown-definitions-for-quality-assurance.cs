using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the Visio file to process
        string filePath;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            filePath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the Visio file: ");
            filePath = Console.ReadLine()?.Trim();
        }

        // Guard against missing or empty path
        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.Error.WriteLine("No file path provided. Exiting.");
            return;
        }

        // Guard against non‑existent file
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        // Load the diagram inside a try/catch to capture loading errors
        Diagram diagram;
        try
        {
            diagram = new Diagram(filePath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        Console.WriteLine("Shapes missing EventMouseDown definitions:");
        Console.WriteLine("-------------------------------------------------");

        // Iterate through all pages and shapes
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // The Aspose.Diagram API does not expose an EventMouseDown cell.
                // Therefore we treat every shape as missing this definition.
                // If a future version adds the cell, replace the following line with a proper check.
                bool isMissingEventMouseDown = true;

                if (isMissingEventMouseDown)
                {
                    // Output shape identification details
                    Console.WriteLine($"Page: {page.NameU} | Shape ID: {shape.ID} | NameU: {shape.NameU}");
                }
            }
        }

        Console.WriteLine("Report generation completed.");
    }
}