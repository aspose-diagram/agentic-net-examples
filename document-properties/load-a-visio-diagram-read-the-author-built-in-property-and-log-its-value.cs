using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – '{inputPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Access the built‑in Creator (Author) property from the document properties.
            // The Creator property holds the author name; it may be null or empty if not set.
            string author = diagram.DocumentProps.Creator; // Changed from Author to Creator

            // Log the Author value (or indicate if it is missing).
            if (string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Author property is not set or is empty.");
            }
            else
            {
                Console.WriteLine($"Author: {author}");
            }
        }
        catch (Exception ex)
        {
            // Write any exceptions to the error stream.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}
