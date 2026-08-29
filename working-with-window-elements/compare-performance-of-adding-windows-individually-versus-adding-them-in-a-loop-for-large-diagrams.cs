using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class WindowPerformanceComparison
{
    static void Main()
    {
        const int windowCount = 5000; // large number of windows for testing

        // -------------------------------------------------
        // Approach 1: Add windows individually (explicit statements)
        // -------------------------------------------------
        var diagramIndividual = new Diagram(); // create a new empty diagram
        var swIndividual = Stopwatch.StartNew();

        // Adding windows one by one using separate statements.
        // In practice this is equivalent to a loop, but we write it explicitly
        // to illustrate the "individual" style.
        for (int i = 0; i < windowCount; i++)
        {
            var win = new Window
            {
                // Set a few properties so the window is not completely empty
                WindowLeft = i % 100,
                WindowTop = i % 100,
                WindowWidth = 800,
                WindowHeight = 600,
                WindowState = 0 // Normal state
            };
            diagramIndividual.Windows.Add(win);
        }

        swIndividual.Stop();
        Console.WriteLine($"Individual addition time: {swIndividual.ElapsedMilliseconds} ms");

        // Save the diagram created with the individual approach
        diagramIndividual.Save("IndividualWindows.vsdx", SaveFileFormat.Vsdx);

        // -------------------------------------------------
        // Approach 2: Add windows inside a tight loop (batch style)
        // -------------------------------------------------
        var diagramLoop = new Diagram(); // create another empty diagram
        var swLoop = Stopwatch.StartNew();

        // Adding windows inside a loop – this is the typical way
        for (int i = 0; i < windowCount; i++)
        {
            var win = new Window
            {
                WindowLeft = i % 100,
                WindowTop = i % 100,
                WindowWidth = 800,
                WindowHeight = 600,
                WindowState = 0
            };
            diagramLoop.Windows.Add(win);
        }

        swLoop.Stop();
        Console.WriteLine($"Loop addition time: {swLoop.ElapsedMilliseconds} ms");

        // Save the diagram created with the loop approach
        diagramLoop.Save("LoopWindows.vsdx", SaveFileFormat.Vsdx);
    }
}
