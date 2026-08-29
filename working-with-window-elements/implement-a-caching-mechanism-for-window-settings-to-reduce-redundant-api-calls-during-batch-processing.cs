using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

namespace DiagramWindowCacheExample
{
    // DTO to hold the relevant window settings
    public class WindowSettings
    {
        public BOOL ShowGrid { get; set; }
        public BOOL ShowGuides { get; set; }
        public BOOL ShowRulers { get; set; }
        public BOOL ShowPageBreaks { get; set; }
        public BOOL DynamicGridEnabled { get; set; }
        public BOOL ShowConnectionPoints { get; set; }
    }

    // Simple cache that stores settings per window ID
    public class WindowSettingsCache
    {
        private readonly Dictionary<int, WindowSettings> _cache = new();

        // Retrieves cached settings or creates a new entry from the window
        public WindowSettings GetOrAdd(Window window)
        {
            if (window == null) throw new ArgumentNullException(nameof(window));

            if (_cache.TryGetValue(window.ID, out var settings))
            {
                return settings;
            }

            // Capture current settings from the window
            settings = new WindowSettings
            {
                ShowGrid = window.ShowGrid,
                ShowGuides = window.ShowGuides,
                ShowRulers = window.ShowRulers,
                ShowPageBreaks = window.ShowPageBreaks,
                DynamicGridEnabled = window.DynamicGridEnabled,
                ShowConnectionPoints = window.ShowConnectionPoints
            };

            _cache[window.ID] = settings;
            return settings;
        }

        // Applies cached settings to a window (used to avoid redundant API calls)
        public void ApplySettings(Window window, WindowSettings settings)
        {
            if (window == null) throw new ArgumentNullException(nameof(window));
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            window.ShowGrid = settings.ShowGrid;
            window.ShowGuides = settings.ShowGuides;
            window.ShowRulers = settings.ShowRulers;
            window.ShowPageBreaks = settings.ShowPageBreaks;
            window.DynamicGridEnabled = settings.DynamicGridEnabled;
            window.ShowConnectionPoints = settings.ShowConnectionPoints;
        }
    }

    class Program
    {
        static void Main()
        {
            // Folder containing Visio files to process
            string folderPath = @"C:\VisioFiles";
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return;
            }

            // Initialize the cache once for the whole batch
            var cache = new WindowSettingsCache();

            // Process each .vsdx file in the folder
            foreach (string filePath in Directory.GetFiles(folderPath, "*.vsdx"))
            {
                Console.WriteLine($"Processing file: {Path.GetFileName(filePath)}");

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Ensure there is at least one window; if not, create a default one
                if (diagram.Windows.Count == 0)
                {
                    var defaultWindow = new Window
                    {
                        WindowState = WindowStateValue.Maximized,
                        WindowWidth = 1100,
                        WindowHeight = 700,
                        WindowType = WindowTypeValue.Drawing
                    };
                    diagram.Windows.Add(defaultWindow);
                }

                // Iterate through all windows in the diagram
                foreach (Window window in diagram.Windows)
                {
                    // Retrieve cached settings or store new ones
                    WindowSettings settings = cache.GetOrAdd(window);

                    // Example usage: apply the cached settings back to the window
                    // (in real scenarios you might apply settings from another diagram)
                    cache.ApplySettings(window, settings);

                    // Output the settings for verification
                    Console.WriteLine($"Window ID {window.ID}: Grid={settings.ShowGrid}, Guides={settings.ShowGuides}, Rulers={settings.ShowRulers}, PageBreaks={settings.ShowPageBreaks}, DynamicGrid={settings.DynamicGridEnabled}, ConnPoints={settings.ShowConnectionPoints}");
                }

                // Optionally save the diagram after processing
                string outputPath = Path.Combine(folderPath, "Processed_" + Path.GetFileName(filePath));
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Saved processed diagram to: {outputPath}");
            }

            Console.WriteLine("Batch processing completed.");
        }
    }
}