using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    // Helper class to store original window settings for rollback
    private class WindowStateSnapshot
    {
        public int Index { get; set; }
        public BOOL ShowGrid { get; set; }
        public BOOL ShowGuides { get; set; }
        public BOOL ShowRulers { get; set; }
        public BOOL ShowPageBreaks { get; set; }
        public BOOL DynamicGridEnabled { get; set; }
        public BOOL ShowConnectionPoints { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        Diagram diagram = null;
        // List to hold original window settings for rollback
        List<WindowStateSnapshot> originalSettings = new List<WindowStateSnapshot>();

        try
        {
            // Load the diagram
            diagram = new Diagram(inputPath);

            // Capture original settings for each window
            for (int i = 0; i < diagram.Windows.Count; i++)
            {
                Window win = diagram.Windows[i];
                originalSettings.Add(new WindowStateSnapshot
                {
                    Index = i,
                    ShowGrid = win.ShowGrid,
                    ShowGuides = win.ShowGuides,
                    ShowRulers = win.ShowRulers,
                    ShowPageBreaks = win.ShowPageBreaks,
                    DynamicGridEnabled = win.DynamicGridEnabled,
                    ShowConnectionPoints = win.ShowConnectionPoints
                });
            }

            // Example processing: modify window visibility settings
            // Here we set all visibility flags to FALSE for demonstration
            for (int i = 0; i < diagram.Windows.Count; i++)
            {
                Window win = diagram.Windows[i];
                win.ShowGrid = BOOL.False;
                win.ShowGuides = BOOL.False;
                win.ShowRulers = BOOL.False;
                win.ShowPageBreaks = BOOL.False;
                win.DynamicGridEnabled = BOOL.False;
                win.ShowConnectionPoints = BOOL.False;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log the error
            Console.Error.WriteLine($"An error occurred: {ex.Message}");

            // Rollback: restore original window settings if diagram was loaded
            if (diagram != null && originalSettings.Count > 0)
            {
                try
                {
                    foreach (var snapshot in originalSettings)
                    {
                        // Ensure the window still exists at the expected index
                        if (snapshot.Index < diagram.Windows.Count)
                        {
                            Window win = diagram.Windows[snapshot.Index];
                            win.ShowGrid = snapshot.ShowGrid;
                            win.ShowGuides = snapshot.ShowGuides;
                            win.ShowRulers = snapshot.ShowRulers;
                            win.ShowPageBreaks = snapshot.ShowPageBreaks;
                            win.DynamicGridEnabled = snapshot.DynamicGridEnabled;
                            win.ShowConnectionPoints = snapshot.ShowConnectionPoints;
                        }
                    }

                    // Attempt to save the rolled-back diagram to a separate file
                    string rollbackPath = Path.Combine(
                        Path.GetDirectoryName(outputPath) ?? "",
                        Path.GetFileNameWithoutExtension(outputPath) + "_rollback" + Path.GetExtension(outputPath));

                    diagram.Save(rollbackPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Rollback completed. Diagram saved to: {rollbackPath}");
                }
                catch (Exception rollbackEx)
                {
                    Console.Error.WriteLine($"Rollback failed: {rollbackEx.Message}");
                }
            }
        }
        finally
        {
            // Dispose diagram if needed
            diagram?.Dispose();
        }
    }
}