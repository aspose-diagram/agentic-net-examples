using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the directory path containing VDX files as the first argument.");
            return;
        }

        string directoryPath = args[0];

        // Guard: ensure the directory exists.
        if (!Directory.Exists(directoryPath))
        {
            Console.Error.WriteLine($"Directory does not exist: {directoryPath}");
            return;
        }

        // Get all .vdx files in the specified directory.
        string[] vdxFiles = Directory.GetFiles(directoryPath, "*.vdx");

        // Process each VDX file.
        foreach (string filePath in vdxFiles)
        {
            // Guard: ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found (skipping): {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram from the VDX file.
                Diagram diagram = new Diagram(filePath);

                // Prepare the current date string (e.g., 2026-10-21).
                string currentDate = DateTime.Now.ToString("d");

                // Update header fields with the current date.
                // HeaderLeft, HeaderCenter, and HeaderRight are string properties.
                diagram.HeaderFooter.HeaderLeft = currentDate;
                diagram.HeaderFooter.HeaderCenter = currentDate;
                diagram.HeaderFooter.HeaderRight = currentDate;

                // Save the modified diagram back to the same file.
                diagram.Save(filePath, SaveFileFormat.Vdx);
                Console.WriteLine($"Successfully updated header for: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during loading, modification, or saving.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}