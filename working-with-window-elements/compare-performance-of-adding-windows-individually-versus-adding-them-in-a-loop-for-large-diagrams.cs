using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        const int windowCount = 5000; // number of windows to add for the test

        // -------------------------------------------------
        // Add windows one by one directly to the diagram
        // -------------------------------------------------
        var swIndividual = Stopwatch.StartNew();
        try
        {
            Diagram diagram1 = new Diagram();
            for (int i = 0; i < windowCount; i++)
            {
                var win = new Window
                {
                    WindowType = WindowTypeValue.Drawing,
                    // WindowState omitted as default is Normal
                    WindowWidth = 800L,
                    WindowHeight = 600L
                };
                diagram1.Windows.Add(win);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during individual window addition: {ex.Message}");
        }
        swIndividual.Stop();
        Console.WriteLine($"Adding {windowCount} windows individually: {swIndividual.ElapsedMilliseconds} ms");

        // -------------------------------------------------
        // Prepare all windows first, then add them in a second loop
        // -------------------------------------------------
        var swBatch = Stopwatch.StartNew();
        try
        {
            Diagram diagram2 = new Diagram();

            // Create all window objects ahead of time
            var windows = new Window[windowCount];
            for (int i = 0; i < windowCount; i++)
            {
                var w = new Window
                {
                    WindowType = WindowTypeValue.Drawing,
                    // WindowState omitted as default is Normal
                    WindowWidth = 800L,
                    WindowHeight = 600L
                };
                windows[i] = w;
            }

            // Add the prepared windows to the diagram
            foreach (var w in windows)
            {
                diagram2.Windows.Add(w);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during batch window addition: {ex.Message}");
        }
        swBatch.Stop();
        Console.WriteLine($"Adding {windowCount} windows in batch: {swBatch.ElapsedMilliseconds} ms");
    }
}