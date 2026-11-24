using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty,
                                                                     Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one window; add a default one if none exist
            if (diagram.Windows.Count == 0)
            {
                Window newWindow = new Window
                {
                    // Set a reasonable default size and state
                    WindowWidth = 1100,
                    WindowHeight = 700,
                    WindowState = (WindowStateValue)0 // Normal state (cast from 0)
                };
                diagram.Windows.Add(newWindow);
            }

            // Iterate over each window and log before/after states
            foreach (Window window in diagram.Windows)
            {
                // Log before changes
                Console.WriteLine($"Window ID: {window.ID}");
                Console.WriteLine("Before Change:");
                LogWindowProperties(window);

                // Apply bulk visibility changes (hide all UI aids)
                window.ShowGrid = BOOL.False;
                window.ShowGuides = BOOL.False;
                window.ShowRulers = BOOL.False;
                window.ShowPageBreaks = BOOL.False;
                window.DynamicGridEnabled = BOOL.False;
                window.ShowConnectionPoints = BOOL.False;

                // Log after changes
                Console.WriteLine("After Change:");
                LogWindowProperties(window);
                Console.WriteLine(new string('-', 40));
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to log relevant window properties
    private static void LogWindowProperties(Window window)
    {
        Console.WriteLine($"  ShowGrid: {(window.ShowGrid == BOOL.True ? "True" : "False")}");
        Console.WriteLine($"  ShowGuides: {(window.ShowGuides == BOOL.True ? "True" : "False")}");
        Console.WriteLine($"  ShowRulers: {(window.ShowRulers == BOOL.True ? "True" : "False")}");
        Console.WriteLine($"  ShowPageBreaks: {(window.ShowPageBreaks == BOOL.True ? "True" : "False")}");
        Console.WriteLine($"  DynamicGridEnabled: {(window.DynamicGridEnabled == BOOL.True ? "True" : "False")}");
        Console.WriteLine($"  ShowConnectionPoints: {(window.ShowConnectionPoints == BOOL.True ? "True" : "False")}");
        Console.WriteLine($"  WindowState: {window.WindowState}");
        Console.WriteLine($"  WindowWidth: {window.WindowWidth}");
        Console.WriteLine($"  WindowHeight: {window.WindowHeight}");
    }
}