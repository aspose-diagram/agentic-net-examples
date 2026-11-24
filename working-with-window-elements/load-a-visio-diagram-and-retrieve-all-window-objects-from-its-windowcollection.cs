using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found -> {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Check if any windows are present
                if (diagram.Windows == null || diagram.Windows.Count == 0)
                {
                    Console.WriteLine("No Window objects found in the diagram.");
                }
                else
                {
                    // Iterate over each Window in the WindowCollection
                    foreach (Window window in diagram.Windows)
                    {
                        // Output basic properties of the window
                        Console.WriteLine($"Window ID: {window.ID}");
                        Console.WriteLine($"Window Type: {window.WindowType}");
                        Console.WriteLine($"Window State: {window.WindowState}");
                        Console.WriteLine($"Window Width: {window.WindowWidth}");
                        Console.WriteLine($"Window Height: {window.WindowHeight}");
                        Console.WriteLine(new string('-', 40));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}