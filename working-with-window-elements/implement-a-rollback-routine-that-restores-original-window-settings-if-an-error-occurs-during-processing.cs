using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Helper class to store a snapshot of a Window's settings
    private class WindowSnapshot
    {
        public int ID { get; set; }
        public WindowTypeValue WindowType { get; set; }
        public long WindowHeight { get; set; }
        public long WindowWidth { get; set; }
        public WindowStateValue WindowState { get; set; }
        public BOOL ShowGrid { get; set; }
        public BOOL ShowGuides { get; set; }
        public BOOL ShowRulers { get; set; }
        public BOOL ShowPageBreaks { get; set; }
        public BOOL DynamicGridEnabled { get; set; }
        public BOOL ShowConnectionPoints { get; set; }
    }

    static void Main(string[] args)
    {
        // Input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the diagram
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Ensure there is at least one window; if none, add a default one
        if (diagram.Windows.Count == 0)
        {
            var defaultWindow = new Window
            {
                WindowType = WindowTypeValue.Drawing,
                WindowState = WindowStateValue.Maximized,
                WindowWidth = 1100,
                WindowHeight = 700
            };
            diagram.Windows.Add(defaultWindow);
        }

        // Capture original settings for all windows
        var originalSnapshots = new WindowSnapshot[diagram.Windows.Count];
        for (int i = 0; i < diagram.Windows.Count; i++)
        {
            var win = diagram.Windows[i];
            originalSnapshots[i] = new WindowSnapshot
            {
                ID = win.ID,
                WindowType = win.WindowType,
                WindowHeight = win.WindowHeight,
                WindowWidth = win.WindowWidth,
                WindowState = win.WindowState,
                ShowGrid = win.ShowGrid,
                ShowGuides = win.ShowGuides,
                ShowRulers = win.ShowRulers,
                ShowPageBreaks = win.ShowPageBreaks,
                DynamicGridEnabled = win.DynamicGridEnabled,
                ShowConnectionPoints = win.ShowConnectionPoints
            };
        }

        try
        {
            // ----- Begin processing that modifies window settings -----
            // Example modifications: hide grid and guides, enable dynamic grid, etc.
            foreach (Window win in diagram.Windows)
            {
                win.ShowGrid = BOOL.False;
                win.ShowGuides = BOOL.False;
                win.ShowRulers = BOOL.False;
                win.ShowPageBreaks = BOOL.False;
                win.DynamicGridEnabled = BOOL.True;
                win.ShowConnectionPoints = BOOL.True;

                // Optionally change size and state
                win.WindowWidth = 1200;
                win.WindowHeight = 800;
                win.WindowState = WindowStateValue.Maximized;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
            Console.Error.WriteLine("Attempting to rollback to original window settings...");

            // Restore original settings
            try
            {
                for (int i = 0; i < diagram.Windows.Count; i++)
                {
                    var win = diagram.Windows[i];
                    var snap = originalSnapshots[i];

                    win.ID = snap.ID;
                    win.WindowType = snap.WindowType;
                    win.WindowHeight = snap.WindowHeight;
                    win.WindowWidth = snap.WindowWidth;
                    win.WindowState = snap.WindowState;
                    win.ShowGrid = snap.ShowGrid;
                    win.ShowGuides = snap.ShowGuides;
                    win.ShowRulers = snap.ShowRulers;
                    win.ShowPageBreaks = snap.ShowPageBreaks;
                    win.DynamicGridEnabled = snap.DynamicGridEnabled;
                    win.ShowConnectionPoints = snap.ShowConnectionPoints;
                }

                // Save the rolled‑back diagram to a separate file to avoid overwriting the failed output
                string rollbackPath = Path.Combine(Path.GetDirectoryName(outputPath) ?? "", 
                    Path.GetFileNameWithoutExtension(outputPath) + "_rollback.vsdx");
                diagram.Save(rollbackPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Rollback completed. Original diagram saved to: {rollbackPath}");
            }
            catch (Exception rollbackEx)
            {
                Console.Error.WriteLine($"Rollback failed: {rollbackEx.Message}");
            }
        }
        finally
        {
            // Dispose diagram if it implements IDisposable (Aspose.Diagram.Diagram does)
            diagram?.Dispose();
        }
    }
}