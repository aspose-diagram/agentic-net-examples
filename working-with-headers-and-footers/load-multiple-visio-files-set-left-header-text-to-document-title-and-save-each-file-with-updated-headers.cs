using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // If no file paths are provided, inform the user and exit.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide one or more Visio file paths as command-line arguments.");
            return;
        }

        // Process each file path supplied via command line.
        foreach (string filePath in args)
        {
            // Guard: ensure the file exists before attempting to load.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram from the specified file.
                Diagram diagram = new Diagram(filePath);

                // Retrieve the document title; if missing, use an empty string.
                string title = diagram.DocumentProps.Title ?? string.Empty;

                // Set the left header text to the document title.
                diagram.HeaderFooter.HeaderLeft = title;

                // Save the diagram back to the same file in VSDX format.
                diagram.Save(filePath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Successfully updated header for: {filePath}");
            }
            catch (Exception ex)
            {
                // Write any errors that occur during processing to the error stream.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}