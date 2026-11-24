using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputLogFile>");
            return;
        }

        string inputPath = args[0];
        string logPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Ensure the directory for the log file exists.
        string logDir = Path.GetDirectoryName(logPath);
        if (!string.IsNullOrEmpty(logDir) && !Directory.Exists(logDir))
        {
            try
            {
                Directory.CreateDirectory(logDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating log directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Open a StreamWriter to write the log file.
            using (StreamWriter writer = new StreamWriter(logPath, false))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through all user‑defined cells of the current shape.
                        foreach (User userCell in shape.Users)
                        {
                            // Retrieve the cell name (universal name) and its current value.
                            string cellName = userCell.NameU ?? string.Empty;
                            string cellValue = userCell.Value?.Val ?? string.Empty;

                            // Write a line to the log file.
                            writer.WriteLine($"Shape ID {shape.ID}, User Cell '{cellName}': '{cellValue}'");
                        }
                    }
                }
            }

            Console.WriteLine($"User‑defined cells have been logged to: {logPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors and write them to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}