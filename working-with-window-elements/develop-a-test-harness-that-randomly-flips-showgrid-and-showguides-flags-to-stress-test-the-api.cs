using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one window to work with
            Window window = new Window();
            window.WindowType = WindowTypeValue.Drawing;
            window.WindowState = WindowStateValue.Maximized;
            window.WindowWidth = 800;
            window.WindowHeight = 600;
            diagram.Windows.Add(window);

            // Random number generator for flipping flags
            Random rnd = new Random();

            // Perform several random flips and save after each change
            for (int i = 0; i < 5; i++)
            {
                // Randomly set ShowGrid
                window.ShowGrid = rnd.Next(2) == 0 ? BOOL.True : BOOL.False;

                // Randomly set ShowGuides
                window.ShowGuides = rnd.Next(2) == 0 ? BOOL.True : BOOL.False;

                // Output current state to console
                Console.WriteLine($"Iteration {i + 1}: ShowGrid = {window.ShowGrid}, ShowGuides = {window.ShowGuides}");

                // Save the diagram to a file to verify the changes
                string outputPath = $"output_{i + 1}.vsdx";
                try
                {
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Saved diagram to {outputPath}");
                }
                catch (Exception ex)
                {
                    // If saving fails, report and stop the test
                    Console.WriteLine($"Error saving diagram: {ex.Message}");
                    throw;
                }
            }

            Console.WriteLine("Stress test completed.");
        }
    }