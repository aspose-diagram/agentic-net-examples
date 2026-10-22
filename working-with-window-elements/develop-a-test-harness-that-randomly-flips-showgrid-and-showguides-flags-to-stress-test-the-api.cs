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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputFolder] [iterations]");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args.Length >= 2 ? args[1] : Path.Combine(Directory.GetCurrentDirectory(), "Output");
        int iterations = args.Length >= 3 && int.TryParse(args[2], out int it) ? it : 10;

        // Guard: input file must exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Load the diagram inside a try/catch block
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Ensure there is at least one window to manipulate
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
                Console.Error.WriteLine($"Error adding default window: {ex.Message}");
                return;
            }
        }

        // Use the first window for flag manipulation
        Window targetWindow = diagram.Windows[0];
        Random rnd = new Random();

        // Stress‑test loop: randomly flip ShowGrid and ShowGuides
        for (int i = 0; i < iterations; i++)
        {
            try
            {
                // Randomly decide the flag values
                BOOL gridValue = rnd.Next(2) == 0 ? BOOL.True : BOOL.False;
                BOOL guidesValue = rnd.Next(2) == 0 ? BOOL.True : BOOL.False;

                // Apply the random values
                targetWindow.ShowGrid = gridValue;
                targetWindow.ShowGuides = guidesValue;

                // Log current iteration state
                Console.WriteLine($"Iteration {i + 1}/{iterations}: ShowGrid = {gridValue}, ShowGuides = {guidesValue}");

                // Save the diagram to a new file for each iteration
                string outputPath = Path.Combine(outputFolder, $"output_{i + 1}.vsdx");
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during iteration {i + 1}: {ex.Message}");
                // Continue with next iteration instead of aborting
            }
        }

        // Cleanup
        diagram?.Dispose();
        Console.WriteLine("Stress test completed.");
    }
}