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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new Window instance and set its properties.
                Window window = new Window();
                window.WindowState = WindowStateValue.Maximized;
                window.WindowHeight = 800;
                window.WindowWidth = 1200;
                window.WindowType = WindowTypeValue.Drawing;

                // Attempt to add the window to the diagram.
                try
                {
                    diagram.Windows.Add(window);
                    Console.WriteLine("Window added successfully.");
                }
                catch (Exception ex)
                {
                    // Handle errors that occur when the WindowCollection is unavailable.
                    Console.Error.WriteLine($"Error adding window: {ex.Message}");
                }

                // Save the modified diagram.
                try
                {
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors during diagram processing.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}