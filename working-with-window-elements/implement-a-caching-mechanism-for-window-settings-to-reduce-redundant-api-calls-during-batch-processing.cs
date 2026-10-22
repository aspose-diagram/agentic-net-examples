using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    // Simple DTO to hold window visibility settings
    private class WindowSettings
    {
        public BOOL ShowGrid { get; set; }
        public BOOL ShowGuides { get; set; }
        public BOOL ShowRulers { get; set; }
        public BOOL ShowPageBreaks { get; set; }
        public BOOL DynamicGridEnabled { get; set; }
        public BOOL ShowConnectionPoints { get; set; }
    }

    static void Main(string[] args)
    {
        // Validate that at least one file path is provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide at least one Visio file path as an argument.");
            return;
        }

        // Cache to store window settings keyed by window ID
        Dictionary<int, WindowSettings> windowCache = new Dictionary<int, WindowSettings>();

        foreach (string inputPath in args)
        {
            // Guard: ensure the file exists before processing
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            try
            {
                // Load the diagram from the file
                Diagram diagram = new Diagram(inputPath);

                // If the diagram has no windows, skip processing
                if (diagram.Windows.Count == 0)
                {
                    Console.WriteLine($"Diagram '{inputPath}' contains no windows. Skipping.");
                    continue;
                }

                // Process each window in the diagram
                foreach (Window window in diagram.Windows)
                {
                    // Use the window's ID as the cache key
                    int windowId = window.ID;

                    // If settings for this window ID are not cached, capture them
                    if (!windowCache.ContainsKey(windowId))
                    {
                        WindowSettings settings = new WindowSettings
                        {
                            ShowGrid = window.ShowGrid,
                            ShowGuides = window.ShowGuides,
                            ShowRulers = window.ShowRulers,
                            ShowPageBreaks = window.ShowPageBreaks,
                            DynamicGridEnabled = window.DynamicGridEnabled,
                            ShowConnectionPoints = window.ShowConnectionPoints
                        };
                        windowCache[windowId] = settings;
                    }

                    // Retrieve cached settings
                    WindowSettings cached = windowCache[windowId];

                    // Apply cached settings to the current window
                    window.ShowGrid = cached.ShowGrid;
                    window.ShowGuides = cached.ShowGuides;
                    window.ShowRulers = cached.ShowRulers;
                    window.ShowPageBreaks = cached.ShowPageBreaks;
                    window.DynamicGridEnabled = cached.DynamicGridEnabled;
                    window.ShowConnectionPoints = cached.ShowConnectionPoints;
                }

                // Prepare output path (same directory with "_out" suffix)
                string directory = Path.GetDirectoryName(inputPath);
                string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_out.vsdx");

                // Save the modified diagram using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processed and saved: {outputPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing
                Console.Error.WriteLine($"Error processing file '{inputPath}': {ex.Message}");
            }
        }
    }
}