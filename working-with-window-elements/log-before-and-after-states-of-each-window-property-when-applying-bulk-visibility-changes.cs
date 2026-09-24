using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one window; add a default one if none exist
            if (diagram.Windows.Count == 0)
            {
                Window defaultWindow = new Window();
                defaultWindow.WindowType = WindowTypeValue.Drawing;
                defaultWindow.WindowState = WindowStateValue.Maximized;
                defaultWindow.WindowWidth = 1100;
                defaultWindow.WindowHeight = 700;
                diagram.Windows.Add(defaultWindow);
            }

            // Apply bulk visibility changes and log before/after states
            foreach (Window window in diagram.Windows)
            {
                // Capture before values
                long beforeHeight = window.WindowHeight;
                long beforeWidth = window.WindowWidth;
                WindowStateValue beforeState = window.WindowState;
                WindowTypeValue beforeType = window.WindowType;
                BOOL beforeShowGrid = window.ShowGrid;
                BOOL beforeShowGuides = window.ShowGuides;
                BOOL beforeShowRulers = window.ShowRulers;
                BOOL beforeShowPageBreaks = window.ShowPageBreaks;
                BOOL beforeDynamicGrid = window.DynamicGridEnabled;
                BOOL beforeShowConnectionPoints = window.ShowConnectionPoints;

                // Log before state
                Console.WriteLine("=== Window ID: {0} ===", window.ID);
                Console.WriteLine("Before - Height: {0}, Width: {1}", beforeHeight, beforeWidth);
                Console.WriteLine("Before - State: {0}, Type: {1}", beforeState, beforeType);
                Console.WriteLine("Before - ShowGrid: {0}, ShowGuides: {1}, ShowRulers: {2}, ShowPageBreaks: {3}",
                    beforeShowGrid, beforeShowGuides, beforeShowRulers, beforeShowPageBreaks);
                Console.WriteLine("Before - DynamicGridEnabled: {0}, ShowConnectionPoints: {1}",
                    beforeDynamicGrid, beforeShowConnectionPoints);

                // Bulk visibility changes (example: hide all UI aids)
                window.ShowGrid = BOOL.False;
                window.ShowGuides = BOOL.False;
                window.ShowRulers = BOOL.False;
                window.ShowPageBreaks = BOOL.False;
                window.DynamicGridEnabled = BOOL.False;
                window.ShowConnectionPoints = BOOL.False;

                // Capture after values
                long afterHeight = window.WindowHeight;
                long afterWidth = window.WindowWidth;
                WindowStateValue afterState = window.WindowState;
                WindowTypeValue afterType = window.WindowType;
                BOOL afterShowGrid = window.ShowGrid;
                BOOL afterShowGuides = window.ShowGuides;
                BOOL afterShowRulers = window.ShowRulers;
                BOOL afterShowPageBreaks = window.ShowPageBreaks;
                BOOL afterDynamicGrid = window.DynamicGridEnabled;
                BOOL afterShowConnectionPoints = window.ShowConnectionPoints;

                // Log after state
                Console.WriteLine("After  - Height: {0}, Width: {1}", afterHeight, afterWidth);
                Console.WriteLine("After  - State: {0}, Type: {1}", afterState, afterType);
                Console.WriteLine("After  - ShowGrid: {0}, ShowGuides: {1}, ShowRulers: {2}, ShowPageBreaks: {3}",
                    afterShowGrid, afterShowGuides, afterShowRulers, afterShowPageBreaks);
                Console.WriteLine("After  - DynamicGridEnabled: {0}, ShowConnectionPoints: {1}",
                    afterDynamicGrid, afterShowConnectionPoints);
                Console.WriteLine();
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
