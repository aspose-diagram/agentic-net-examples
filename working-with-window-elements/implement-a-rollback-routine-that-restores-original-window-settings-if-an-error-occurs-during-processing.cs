using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace DiagramWindowRollback
{
    // Helper class to store window settings for rollback
    class WindowSettings
    {
        public int Id { get; set; }
        public long Height { get; set; }
        public long Width { get; set; }
        public WindowStateValue State { get; set; }
        public WindowTypeValue Type { get; set; }
        public BOOL ShowGrid { get; set; }
        public BOOL ShowGuides { get; set; }
        public BOOL ShowRulers { get; set; }
        public BOOL ShowPageBreaks { get; set; }
        public BOOL DynamicGridEnabled { get; set; }
        public BOOL ShowConnectionPoints { get; set; }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load diagram
                Diagram diagram = new Diagram(inputPath);

                // Capture original window settings
                List<WindowSettings> originalSettings = new List<WindowSettings>();
                foreach (Window win in diagram.Windows)
                {
                    originalSettings.Add(new WindowSettings
                    {
                        Id = win.ID,
                        Height = win.WindowHeight,
                        Width = win.WindowWidth,
                        State = win.WindowState,
                        Type = win.WindowType,
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
                    // Example processing: modify some window properties
                    foreach (Window win in diagram.Windows)
                    {
                        win.ShowGrid = BOOL.False;
                        win.ShowGuides = BOOL.False;
                        win.ShowRulers = BOOL.False;
                        win.ShowPageBreaks = BOOL.False;
                        win.DynamicGridEnabled = BOOL.False;
                        win.ShowConnectionPoints = BOOL.False;
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    // Rollback to original settings
                    foreach (WindowSettings ws in originalSettings)
                    {
                        // Find the window by ID
                        Window win = null;
                        foreach (Window w in diagram.Windows)
                        {
                            if (w.ID == ws.Id)
                            {
                                win = w;
                                break;
                            }
                        }

                        if (win != null)
                        {
                            win.WindowHeight = ws.Height;
                            win.WindowWidth = ws.Width;
                            win.WindowState = ws.State;
                            win.WindowType = ws.Type;
                            win.ShowGrid = ws.ShowGrid;
                            win.ShowGuides = ws.ShowGuides;
                            win.ShowRulers = ws.ShowRulers;
                            win.ShowPageBreaks = ws.ShowPageBreaks;
                            win.DynamicGridEnabled = ws.DynamicGridEnabled;
                            win.ShowConnectionPoints = ws.ShowConnectionPoints;
                        }
                    }

                    // Optionally re-save the rolled-back diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine("Original window settings have been restored.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}