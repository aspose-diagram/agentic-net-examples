using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one window to work with
            if (diagram.Windows.Count == 0)
            {
                Window window = new Window();
                window.WindowType = WindowTypeValue.Drawing;
                window.WindowState = WindowStateValue.Maximized;
                window.WindowWidth = 1100;
                window.WindowHeight = 700;
                diagram.Windows.Add(window);
            }

            Random random = new Random();
            int iterations = 20;

            for (int i = 0; i < iterations; i++)
            {
                // Randomly choose BOOL values for ShowGrid and ShowGuides
                BOOL showGrid = random.Next(2) == 0 ? BOOL.True : BOOL.False;
                BOOL showGuides = random.Next(2) == 0 ? BOOL.True : BOOL.False;

                // Apply the random settings to the first window
                diagram.Windows[0].ShowGrid = showGrid;
                diagram.Windows[0].ShowGuides = showGuides;

                Console.WriteLine($"Iteration {i + 1}: ShowGrid={showGrid}, ShowGuides={showGuides}");

                // Validate by saving the diagram to a memory stream
                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        diagram.Save(ms, SaveFileFormat.Vsdx);
                        Console.WriteLine($"Saved diagram size: {ms.Length} bytes");
                    }
                }
                catch (Exception ex)
                {
                    // Report any failure and stop the test
                    Console.WriteLine($"Error during save on iteration {i + 1}: {ex.Message}");
                    throw;
                }
            }

            Console.WriteLine("Stress test completed successfully.");
        }
    }