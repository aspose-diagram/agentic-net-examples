using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

namespace DiagramWindowRollback
{
    // Snapshot of a Window's relevant properties for rollback
    class WindowSnapshot
    {
        public int ID { get; }
        public BOOL ShowGrid { get; }
        public BOOL ShowGuides { get; }
        public BOOL ShowRulers { get; }
        public BOOL ShowPageBreaks { get; }
        public BOOL DynamicGridEnabled { get; }
        public BOOL ShowConnectionPoints { get; }
        public long WindowHeight { get; }
        public long WindowWidth { get; }
        public WindowStateValue WindowState { get; }
        public WindowTypeValue WindowType { get; }

        public WindowSnapshot(Window window)
        {
            // Capture all relevant properties from the source window
            ID = window.ID;
            ShowGrid = window.ShowGrid;
            ShowGuides = window.ShowGuides;
            ShowRulers = window.ShowRulers;
            ShowPageBreaks = window.ShowPageBreaks;
            DynamicGridEnabled = window.DynamicGridEnabled;
            ShowConnectionPoints = window.ShowConnectionPoints;
            WindowHeight = window.WindowHeight;
            WindowWidth = window.WindowWidth;
            WindowState = window.WindowState;
            WindowType = window.WindowType;
        }

        // Apply stored values back to a Window instance
        public void Apply(Window window)
        {
            window.ShowGrid = ShowGrid;
            window.ShowGuides = ShowGuides;
            window.ShowRulers = ShowRulers;
            window.ShowPageBreaks = ShowPageBreaks;
            window.DynamicGridEnabled = DynamicGridEnabled;
            window.ShowConnectionPoints = ShowConnectionPoints;
            window.WindowHeight = WindowHeight;
            window.WindowWidth = WindowWidth;
            window.WindowState = WindowState;
            window.WindowType = WindowType;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            // Guard to ensure the input file exists before proceeding
            if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

            string outputPath = "output.vsdx";

            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Capture original window settings for potential rollback
            List<WindowSnapshot> originalWindows = new List<WindowSnapshot>();
            foreach (Window win in diagram.Windows)
            {
                originalWindows.Add(new WindowSnapshot(win));
            }

            try
            {
                // Example processing: modify some window settings
                foreach (Window win in diagram.Windows)
                {
                    win.ShowGrid = BOOL.False;
                    win.ShowGuides = BOOL.False;
                    win.ShowRulers = BOOL.False;
                    win.ShowPageBreaks = BOOL.False;
                    win.DynamicGridEnabled = BOOL.False;
                    win.ShowConnectionPoints = BOOL.False;
                    win.WindowState = WindowStateValue.Maximized;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Log the error and initiate rollback
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine("Restoring original window settings...");

                // Restore each window using its snapshot (matched by ID)
                foreach (WindowSnapshot snapshot in originalWindows)
                {
                    // Find the window with the same ID by iterating the collection
                    Window win = null;
                    foreach (Window w in diagram.Windows)
                    {
                        if (w.ID == snapshot.ID)
                        {
                            win = w;
                            break;
                        }
                    }

                    // If a matching window is found, apply the snapshot
                    if (win != null)
                    {
                        snapshot.Apply(win);
                    }
                }

                // Re-save the diagram after rollback
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Rollback completed and diagram saved.");
            }
        }
    }
}