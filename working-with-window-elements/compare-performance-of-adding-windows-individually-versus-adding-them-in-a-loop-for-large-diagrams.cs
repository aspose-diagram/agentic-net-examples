using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Number of windows to add – large enough to notice timing differences
        const int windowCount = 5000;

        // ------------------------------------------------------------
        // Method A – Add a window and immediately save the diagram.
        // This simulates “adding windows individually”.
        // ------------------------------------------------------------
        Diagram diagramA = new Diagram(); // create a new diagram
        Stopwatch swIndividual = Stopwatch.StartNew();

        for (int i = 0; i < windowCount; i++)
        {
            // Create a new window and set a couple of properties
            Window win = new Window
            {
                WindowLeft = i,
                WindowTop = i,
                WindowWidth = 5,
                WindowHeight = 5,
                WindowState = 0 // Normal state
            };

            // Add the window to the diagram
            diagramA.Windows.Add(win);

            // Save after each addition – this is the costly part
            diagramA.Save($"temp_individual_{i}.vdx", SaveFileFormat.Vdx);
        }

        swIndividual.Stop();

        // ------------------------------------------------------------
        // Method B – Add all windows first, then save once.
        // This simulates “adding them in a loop”.
        // ------------------------------------------------------------
        Diagram diagramB = new Diagram(); // create another diagram
        Stopwatch swBatch = Stopwatch.StartNew();

        for (int i = 0; i < windowCount; i++)
        {
            Window win = new Window
            {
                WindowLeft = i,
                WindowTop = i,
                WindowWidth = 5,
                WindowHeight = 5,
                WindowState = 0
            };

            diagramB.Windows.Add(win);
        }

        // Save only once after all windows have been added
        diagramB.Save("output_batch.vdx", SaveFileFormat.Vdx);
        swBatch.Stop();

        // ------------------------------------------------------------
        // Output the measured times
        // ------------------------------------------------------------
        Console.WriteLine($"Adding windows individually (save each time): {swIndividual.ElapsedMilliseconds} ms");
        Console.WriteLine($"Adding windows in a batch (single save): {swBatch.ElapsedMilliseconds} ms");
    }
}
