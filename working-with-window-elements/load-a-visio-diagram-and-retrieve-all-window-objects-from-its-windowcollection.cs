using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file – replace with your actual file path or pass as an argument.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard against missing file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there are windows to enumerate.
                if (diagram.Windows == null || diagram.Windows.Count == 0)
                {
                    Console.WriteLine("No windows found in the diagram.");
                    return;
                }

                // Iterate over each Window object in the WindowCollection.
                foreach (Window window in diagram.Windows)
                {
                    // Output basic window information.
                    Console.WriteLine("Window ID: " + window.ID);
                    Console.WriteLine("Window Type: " + window.WindowType);
                    Console.WriteLine("Window Width: " + window.WindowWidth);
                    Console.WriteLine("Window Height: " + window.WindowHeight);
                    Console.WriteLine("Window State: " + window.WindowState);
                    Console.WriteLine(new string('-', 40));
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine("An error occurred while processing the diagram:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}