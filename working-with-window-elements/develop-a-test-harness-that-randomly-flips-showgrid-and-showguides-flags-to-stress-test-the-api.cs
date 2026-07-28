using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one window; add a drawing window if none exist
            if (diagram.Windows.Count == 0)
            {
                Window window = new Window();
                window.WindowType = WindowTypeValue.Drawing;
                window.WindowState = WindowStateValue.Maximized;
                window.WindowWidth = 1200;
                window.WindowHeight = 800;
                diagram.Windows.Add(window);
            }

            // Get the first window (the one we will manipulate)
            Window targetWindow = diagram.Windows[0];

            // Random number generator for flipping flags
            Random rnd = new Random();

            // Number of iterations for stress testing
            int iterations = 20;

            for (int i = 0; i < iterations; i++)
            {
                // Randomly decide the flag values
                BOOL showGrid = rnd.Next(2) == 0 ? BOOL.True : BOOL.False;
                BOOL showGuides = rnd.Next(2) == 0 ? BOOL.True : BOOL.False;

                // Apply the random values
                targetWindow.ShowGrid = showGrid;
                targetWindow.ShowGuides = showGuides;

                // Output the current state to the console
                Console.WriteLine($"Iteration {i + 1}: ShowGrid = {targetWindow.ShowGrid}, ShowGuides = {targetWindow.ShowGuides}");
            }

            // Save the resulting diagram to a file for verification
            string outputPath = "StressTestResult.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }