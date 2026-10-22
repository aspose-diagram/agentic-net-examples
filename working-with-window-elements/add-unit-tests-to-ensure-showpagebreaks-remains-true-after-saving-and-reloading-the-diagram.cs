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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the original diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one window; create one if necessary.
                if (diagram.Windows.Count == 0)
                {
                    Window newWindow = new Window();
                    newWindow.WindowType = WindowTypeValue.Drawing;
                    newWindow.WindowState = WindowStateValue.Maximized;
                    diagram.Windows.Add(newWindow);
                }

                // Access the first window and set ShowPageBreaks to TRUE.
                Window window = diagram.Windows[0];
                window.ShowPageBreaks = BOOL.True;

                // Save the diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Reload the saved diagram to verify the property persisted.
            using (Diagram reloadedDiagram = new Diagram(outputPath))
            {
                // Guard against missing window after reload.
                if (reloadedDiagram.Windows.Count == 0)
                {
                    throw new Exception("Reloaded diagram contains no windows.");
                }

                Window reloadedWindow = reloadedDiagram.Windows[0];

                // Verify that ShowPageBreaks is still TRUE.
                if (reloadedWindow.ShowPageBreaks != BOOL.True)
                {
                    throw new Exception("ShowPageBreaks property was not preserved after saving and reloading.");
                }

                // If we reach this point, the test succeeded.
                Console.WriteLine("Test passed: ShowPageBreaks remains TRUE after save and reload.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}