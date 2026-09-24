using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Snapshot of window settings to allow rollback
    class WindowSnapshot
    {
        public int ID;
        public WindowTypeValue Type;
        public long Height;
        public long Width;
        public WindowStateValue State;
        public BOOL ShowGrid;
        public BOOL ShowGuides;
        public BOOL ShowRulers;
        public BOOL ShowPageBreaks;
        public BOOL DynamicGridEnabled;
        public BOOL ShowConnectionPoints;
    }

    static void Main()
    {
        try
        {

            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one window; if not, create a default one
            if (diagram.Windows.Count == 0)
            {
                Window defaultWindow = new Window();
                defaultWindow.WindowType = WindowTypeValue.Drawing;
                defaultWindow.WindowState = WindowStateValue.Maximized;
                defaultWindow.WindowWidth = 1100;
                defaultWindow.WindowHeight = 700;
                diagram.Windows.Add(defaultWindow);
            }

            // Capture original window settings
            var originalSettings = new System.Collections.Generic.List<WindowSnapshot>();
            foreach (Window win in diagram.Windows)
            {
                originalSettings.Add(new WindowSnapshot
                {
                    ID = win.ID,
                    Type = win.WindowType,
                    Height = win.WindowHeight,
                    Width = win.WindowWidth,
                    State = win.WindowState,
                    ShowGrid = win.ShowGrid,
                    ShowGuides = win.ShowGuides,
                    ShowRulers = win.ShowRulers,
                    ShowPageBreaks = win.ShowPageBreaks,
                    DynamicGridEnabled = win.DynamicGridEnabled,
                    ShowConnectionPoints = win.ShowConnectionPoints
                });
            }

            try
            {
                // Example processing that modifies window settings
                foreach (Window win in diagram.Windows)
                {
                    win.ShowGrid = BOOL.False;
                    win.ShowGuides = BOOL.False;
                    win.ShowRulers = BOOL.False;
                    win.ShowPageBreaks = BOOL.False;
                    win.DynamicGridEnabled = BOOL.False;
                    win.ShowConnectionPoints = BOOL.False;
                    win.WindowState = WindowStateValue.Minimized;
                }

                // Simulate an error (remove this line in production)
                int triggerError = 0;
                int fail = 10 / triggerError; // This will throw DivideByZeroException
            }
            catch (Exception ex)
            {
                // Rollback to original settings
                for (int i = 0; i < diagram.Windows.Count && i < originalSettings.Count; i++)
                {
                    Window win = diagram.Windows[i];
                    WindowSnapshot snap = originalSettings[i];

                    win.ID = snap.ID;
                    win.WindowType = snap.Type;
                    win.WindowHeight = snap.Height;
                    win.WindowWidth = snap.Width;
                    win.WindowState = snap.State;
                    win.ShowGrid = snap.ShowGrid;
                    win.ShowGuides = snap.ShowGuides;
                    win.ShowRulers = snap.ShowRulers;
                    win.ShowPageBreaks = snap.ShowPageBreaks;
                    win.DynamicGridEnabled = snap.DynamicGridEnabled;
                    win.ShowConnectionPoints = snap.ShowConnectionPoints;
                }

                Console.WriteLine("An error occurred: " + ex.Message);
                Console.WriteLine("Window settings have been restored to their original values.");
            }

            // Save the diagram (whether modified or rolled back)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}