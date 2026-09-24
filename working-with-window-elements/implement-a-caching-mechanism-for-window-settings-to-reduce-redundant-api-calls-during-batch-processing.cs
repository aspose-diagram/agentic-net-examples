using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace DiagramWindowCacheExample
{
    // Represents the settings of a diagram window that we want to cache.
    public class WindowSettings
    {
        public WindowStateValue State { get; set; }
        public long Height { get; set; }
        public long Width { get; set; }
        public WindowTypeValue Type { get; set; }
        public BOOL ShowGrid { get; set; }
        public BOOL ShowGuides { get; set; }
        public BOOL ShowRulers { get; set; }
        public BOOL ShowPageBreaks { get; set; }
        public BOOL DynamicGridEnabled { get; set; }
        public BOOL ShowConnectionPoints { get; set; }
    }

    // Simple cache that stores window settings keyed by window ID.
    public class WindowSettingsCache
    {
        private readonly Dictionary<int, WindowSettings> _cache = new();

        // Capture settings from a Window instance and store them in the cache.
        public void AddOrUpdate(Window window)
        {
            if (window == null) throw new ArgumentNullException(nameof(window));

            var settings = new WindowSettings
            {
                State = window.WindowState,
                Height = window.WindowHeight,
                Width = window.WindowWidth,
                Type = window.WindowType,
                ShowGrid = window.ShowGrid,
                ShowGuides = window.ShowGuides,
                ShowRulers = window.ShowRulers,
                ShowPageBreaks = window.ShowPageBreaks,
                DynamicGridEnabled = window.DynamicGridEnabled,
                ShowConnectionPoints = window.ShowConnectionPoints
            };

            _cache[window.ID] = settings;
        }

        // Apply cached settings to a Window instance if a matching entry exists.
        public void Apply(Window window)
        {
            if (window == null) throw new ArgumentNullException(nameof(window));

            if (_cache.TryGetValue(window.ID, out var settings))
            {
                window.WindowState = settings.State;
                window.WindowHeight = settings.Height;
                window.WindowWidth = settings.Width;
                window.WindowType = settings.Type;
                window.ShowGrid = settings.ShowGrid;
                window.ShowGuides = settings.ShowGuides;
                window.ShowRulers = settings.ShowRulers;
                window.ShowPageBreaks = settings.ShowPageBreaks;
                window.DynamicGridEnabled = settings.DynamicGridEnabled;
                window.ShowConnectionPoints = settings.ShowConnectionPoints;
            }
            else
            {
                // No cached settings for this window; optionally handle this case.
                Console.WriteLine($"No cached settings found for window ID {window.ID}.");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing diagram (replace with actual file path).
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Initialize the cache and capture current window settings.
                var cache = new WindowSettingsCache();
                foreach (Window win in diagram.Windows)
                {
                    cache.AddOrUpdate(win);
                }

                // Example modification: change some window properties for demonstration.
                foreach (Window win in diagram.Windows)
                {
                    win.WindowState = WindowStateValue.Maximized;
                    win.ShowGrid = BOOL.False;
                    win.ShowGuides = BOOL.False;
                }

                // Later in the batch process we may want to restore the original settings.
                foreach (Window win in diagram.Windows)
                {
                    cache.Apply(win);
                }

                // Save the diagram after processing.
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}