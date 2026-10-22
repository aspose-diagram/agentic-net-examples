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
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(filePath);

            // Iterate through each Window in the diagram
            foreach (Window window in diagram.Windows)
            {
                // Log window identifier
                Console.WriteLine($"Window ID: {window.ID}");

                // Log ShowGrid, ShowGuides, ShowRulers, ShowPageBreaks values
                Console.WriteLine($"ShowGrid: {window.ShowGrid}");
                Console.WriteLine($"ShowGuides: {window.ShowGuides}");
                Console.WriteLine($"ShowRulers: {window.ShowRulers}");
                Console.WriteLine($"ShowPageBreaks: {window.ShowPageBreaks}");
                Console.WriteLine(); // Blank line for readability
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}