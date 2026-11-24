using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the diagram inside a try/catch to capture any Aspose errors.
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Ensure there is at least one window; otherwise add a default window.
        if (diagram.Windows.Count == 0)
        {
            try
            {
                Window defaultWindow = new Window();
                defaultWindow.WindowType = WindowTypeValue.Drawing;
                defaultWindow.WindowState = WindowStateValue.Maximized;
                defaultWindow.WindowWidth = 1100;
                defaultWindow.WindowHeight = 700;
                diagram.Windows.Add(defaultWindow);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to add default window: {ex.Message}");
                return;
            }
        }

        // Use the first window for the test.
        Window testWindow = diagram.Windows[0];

        Random rnd = new Random();
        int iterations = 10; // Number of random flips.

        for (int i = 0; i < iterations; i++)
        {
            // Randomly decide the BOOL values for ShowGrid and ShowGuides.
            BOOL gridValue = rnd.Next(2) == 0 ? BOOL.True : BOOL.False;
            BOOL guidesValue = rnd.Next(2) == 0 ? BOOL.True : BOOL.False;

            // Apply the random settings.
            try
            {
                testWindow.ShowGrid = gridValue;
                testWindow.ShowGuides = guidesValue;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Iteration {i + 1}: Failed to set window flags – {ex.Message}");
                continue;
            }

            // Prepare an output file name for this iteration.
            string outputDir = Path.GetDirectoryName(inputPath) ?? ".";
            string outputPath = Path.Combine(outputDir, $"output_{i + 1}.vsdx");

            // Save the diagram with the current settings.
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Iteration {i + 1}: Saved to '{outputPath}' with ShowGrid={gridValue}, ShowGuides={guidesValue}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Iteration {i + 1}: Save failed – {ex.Message}");
            }
        }

        // Clean up the diagram object if it implements IDisposable.
        if (diagram is IDisposable disposableDiagram)
        {
            disposableDiagram.Dispose();
        }
    }
}