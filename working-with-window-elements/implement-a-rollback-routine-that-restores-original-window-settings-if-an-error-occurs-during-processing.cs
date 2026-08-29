using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

public class DiagramProcessor
{
    /// <summary>
    /// Loads a Visio diagram, performs processing, and rolls back window settings if an error occurs.
    /// </summary>
    /// <param name="inputPath">Path to the source .vsdx file.</param>
    /// <param name="outputPath">Path where the processed file should be saved.</param>
    public static void ProcessDiagram(string inputPath, string outputPath)
    {
        // Load the diagram (lifecycle rule: load)
        Diagram diagram = new Diagram(inputPath);

        // Preserve original window settings
        List<Window> originalWindows = new List<Window>();
        foreach (Window win in diagram.Windows)
        {
            // Deep copy of the window's relevant properties
            Window copy = new Window
            {
                ID = win.ID,
                WindowState = win.WindowState,
                WindowLeft = win.WindowLeft,
                WindowTop = win.WindowTop,
                WindowWidth = win.WindowWidth,
                WindowHeight = win.WindowHeight,
                ShowGrid = win.ShowGrid,
                ShowGuides = win.ShowGuides,
                ShowRulers = win.ShowRulers,
                ShowConnectionPoints = win.ShowConnectionPoints,
                ShowPageBreaks = win.ShowPageBreaks,
                ViewScale = win.ViewScale,
                ViewCenterX = win.ViewCenterX,
                ViewCenterY = win.ViewCenterY,
                // Add any other properties that are important for your scenario
            };
            originalWindows.Add(copy);
        }

        try
        {
            // ----- Begin processing logic -----
            // Example: modify window settings (replace with real processing)
            foreach (Window win in diagram.Windows)
            {
                // Example modification: maximize all windows
                win.WindowState = WindowStateValue.Maximized;
            }

            // Additional processing steps can be placed here.
            // If any step throws, the catch block will restore the original settings.
            // ----- End processing logic -----
        }
        catch (Exception ex)
        {
            // Rollback: restore the original window collection
            diagram.Windows.Clear();
            foreach (Window original in originalWindows)
            {
                // Add a fresh copy to avoid reference issues
                Window restored = new Window
                {
                    ID = original.ID,
                    WindowState = original.WindowState,
                    WindowLeft = original.WindowLeft,
                    WindowTop = original.WindowTop,
                    WindowWidth = original.WindowWidth,
                    WindowHeight = original.WindowHeight,
                    ShowGrid = original.ShowGrid,
                    ShowGuides = original.ShowGuides,
                    ShowRulers = original.ShowRulers,
                    ShowConnectionPoints = original.ShowConnectionPoints,
                    ShowPageBreaks = original.ShowPageBreaks,
                    ViewScale = original.ViewScale,
                    ViewCenterX = original.ViewCenterX,
                    ViewCenterY = original.ViewCenterY
                };
                diagram.Windows.Add(restored);
            }

            // Optionally rethrow or handle the exception as needed
            Console.WriteLine($"Error during processing: {ex.Message}");
            // Rethrow to inform caller that processing failed
            throw;
        }

        // Save the diagram (lifecycle rule: save)
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramProcessor.ProcessDiagram("", "");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
