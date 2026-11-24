using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new Window instance and set its properties
                Window newWindow = new Window();
                newWindow.WindowState = WindowStateValue.Maximized;
                newWindow.WindowHeight = 800;
                newWindow.WindowWidth = 1200;
                newWindow.WindowType = WindowTypeValue.Drawing;

                // Attempt to add the window to the diagram
                try
                {
                    // If the diagram somehow lacks a WindowCollection, this will throw
                    diagram.Windows.Add(newWindow);
                    Console.WriteLine("Window added successfully.");
                }
                catch (Exception ex)
                {
                    // Catch and report any exception that occurs during the add operation
                    Console.Error.WriteLine($"Error adding window: {ex.Message}");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors during loading or saving
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}