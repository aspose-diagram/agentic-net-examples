using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(filePath);

            // Check if any windows are present.
            if (diagram.Windows == null || diagram.Windows.Count == 0)
            {
                Console.WriteLine("No windows found in the diagram.");
                return;
            }

            // Iterate through each Window and log the required BOOL properties.
            foreach (Window window in diagram.Windows)
            {
                // Log window identifier for clarity.
                Console.WriteLine($"Window ID: {window.ID}");

                // Log ShowGrid value.
                Console.WriteLine($"  ShowGrid: {window.ShowGrid}");

                // Log ShowGuides value.
                Console.WriteLine($"  ShowGuides: {window.ShowGuides}");

                // Log ShowRulers value.
                Console.WriteLine($"  ShowRulers: {window.ShowRulers}");

                // Log ShowPageBreaks value.
                Console.WriteLine($"  ShowPageBreaks: {window.ShowPageBreaks}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}