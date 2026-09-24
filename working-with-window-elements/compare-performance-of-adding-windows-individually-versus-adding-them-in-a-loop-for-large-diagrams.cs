using System.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        const int windowCount = 10000;

        // Measure adding windows individually (directly to the diagram)
        var swIndividual = Stopwatch.StartNew();
        AddWindowsIndividually(windowCount);
        swIndividual.Stop();
        Console.WriteLine($"Adding {windowCount} windows individually took: {swIndividual.ElapsedMilliseconds} ms");

        // Measure adding windows in a batch (create list first, then add to diagram)
        var swBatch = Stopwatch.StartNew();
        AddWindowsInBatch(windowCount);
        swBatch.Stop();
        Console.WriteLine($"Adding {windowCount} windows in batch took: {swBatch.ElapsedMilliseconds} ms");
    }

    // Adds windows one by one directly to the diagram
    private static void AddWindowsIndividually(int count)
    {
        var diagram = new Diagram();

        for (int i = 0; i < count; i++)
        {
            var window = new Window
            {
                WindowType = WindowTypeValue.Drawing,
                WindowState = WindowStateValue.Maximized,
                WindowWidth = 800L,
                WindowHeight = 600L,
                DynamicGridEnabled = BOOL.True,
                ShowGrid = BOOL.True,
                ShowGuides = BOOL.True,
                ShowRulers = BOOL.True,
                ShowPageBreaks = BOOL.True,
                ShowConnectionPoints = BOOL.True
            };

            diagram.Windows.Add(window);
        }
    }

    // Creates all windows first, then adds them to the diagram in a second loop
    private static void AddWindowsInBatch(int count)
    {
        var diagram = new Diagram();
        var windows = new List<Window>(count);

        // First loop: create windows and store them in a list
        for (int i = 0; i < count; i++)
        {
            var window = new Window
            {
                WindowType = WindowTypeValue.Drawing,
                WindowState = WindowStateValue.Maximized,
                WindowWidth = 800L,
                WindowHeight = 600L,
                DynamicGridEnabled = BOOL.True,
                ShowGrid = BOOL.True,
                ShowGuides = BOOL.True,
                ShowRulers = BOOL.True,
                ShowPageBreaks = BOOL.True,
                ShowConnectionPoints = BOOL.True
            };

            windows.Add(window);
        }

        // Second loop: add all windows to the diagram
        foreach (var window in windows)
        {
            diagram.Windows.Add(window);
        }
    }
}
