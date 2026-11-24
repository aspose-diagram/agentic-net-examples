using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (must exist)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Number of windows to add for the performance test
        int windowCount = 1000;

        // ------------------------------------------------------------
        // Approach 1: Add windows directly to the diagram one by one
        // ------------------------------------------------------------
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                Stopwatch sw = Stopwatch.StartNew();

                for (int i = 0; i < windowCount; i++)
                {
                    // Create a new window instance
                    Window win = new Window();

                    // Configure basic window properties
                    win.WindowType = WindowTypeValue.Drawing;
                    // Normal state is represented by the integer value 0
                    win.WindowState = (WindowStateValue)0;
                    win.WindowWidth = 800;
                    win.WindowHeight = 600;

                    // Add the window to the diagram's window collection
                    diagram.Windows.Add(win);
                }

                sw.Stop();
                Console.WriteLine($"Approach 1 (individual adds) elapsed: {sw.ElapsedMilliseconds} ms");

                // Save the resulting diagram
                diagram.Save("individual_windows.vsdx", SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during Approach 1: {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // Approach 2: Create all windows first, then add them in a batch loop
        // ------------------------------------------------------------
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                Stopwatch sw = Stopwatch.StartNew();

                // First, create all windows and store them in a list
                List<Window> windows = new List<Window>(windowCount);
                for (int i = 0; i < windowCount; i++)
                {
                    Window win = new Window();
                    win.WindowType = WindowTypeValue.Drawing;
                    win.WindowState = (WindowStateValue)0;
                    win.WindowWidth = 800;
                    win.WindowHeight = 600;
                    windows.Add(win);
                }

                // Now add all prepared windows to the diagram
                foreach (Window win in windows)
                {
                    diagram.Windows.Add(win);
                }

                sw.Stop();
                Console.WriteLine($"Approach 2 (batch add) elapsed: {sw.ElapsedMilliseconds} ms");

                // Save the resulting diagram
                diagram.Save("batch_windows.vsdx", SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during Approach 2: {ex.Message}");
            return;
        }
    }
}