using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path (overwrite input if not specified)
        string outputPath = args.Length > 1 ? args[1] : inputPath;

        try
        {
            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure at least one window exists; create a default one if needed
                if (diagram.Windows.Count == 0)
                {
                    Window defaultWindow = new Window();
                    defaultWindow.WindowType = WindowTypeValue.Drawing;
                    defaultWindow.WindowState = (WindowStateValue)0; // Normal state
                    defaultWindow.WindowWidth = 1100;
                    defaultWindow.WindowHeight = 700;
                    diagram.Windows.Add(defaultWindow);
                }

                // Restore default window settings for each window
                foreach (Window window in diagram.Windows)
                {
                    // Show grid, guides, rulers, and page breaks
                    window.ShowGrid = BOOL.True;
                    window.ShowGuides = BOOL.True;
                    window.ShowRulers = BOOL.True;
                    window.ShowPageBreaks = BOOL.True;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}