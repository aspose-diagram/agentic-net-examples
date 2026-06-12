using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace DiagramWindowCacheDemo
{
    // DTO to hold window visibility and grid settings
    public class WindowSettings
    {
        public BOOL ShowGrid { get; set; }
        public BOOL ShowGuides { get; set; }
        public BOOL ShowRulers { get; set; }
        public BOOL ShowPageBreaks { get; set; }
        public BOOL DynamicGridEnabled { get; set; }
        public BOOL ShowConnectionPoints { get; set; }

        // Clone method to create a copy of settings
        public WindowSettings Clone()
        {
            return new WindowSettings
            {
                ShowGrid = this.ShowGrid,
                ShowGuides = this.ShowGuides,
                ShowRulers = this.ShowRulers,
                ShowPageBreaks = this.ShowPageBreaks,
                DynamicGridEnabled = this.DynamicGridEnabled,
                ShowConnectionPoints = this.ShowConnectionPoints
            };
        }
    }

    // Simple cache for window settings keyed by Window.ID
    public class WindowSettingsCache
    {
        private readonly Dictionary<int, WindowSettings> _cache = new();

        // Load all window settings from the diagram into the cache
        public void LoadCache(Diagram diagram)
        {
            foreach (Window window in diagram.Windows)
            {
                var settings = new WindowSettings
                {
                    ShowGrid = window.ShowGrid,
                    ShowGuides = window.ShowGuides,
                    ShowRulers = window.ShowRulers,
                    ShowPageBreaks = window.ShowPageBreaks,
                    DynamicGridEnabled = window.DynamicGridEnabled,
                    ShowConnectionPoints = window.ShowConnectionPoints
                };
                _cache[window.ID] = settings;
            }
        }

        // Retrieve cached settings for a given window ID; returns null if not cached
        public WindowSettings GetSettings(int windowId)
        {
            return _cache.TryGetValue(windowId, out var settings) ? settings.Clone() : null;
        }

        // Update the cache after a window's settings have been changed
        public void UpdateCache(Window window)
        {
            if (_cache.ContainsKey(window.ID))
            {
                var settings = new WindowSettings
                {
                    ShowGrid = window.ShowGrid,
                    ShowGuides = window.ShowGuides,
                    ShowRulers = window.ShowRulers,
                    ShowPageBreaks = window.ShowPageBreaks,
                    DynamicGridEnabled = window.DynamicGridEnabled,
                    ShowConnectionPoints = window.ShowConnectionPoints
                };
                _cache[window.ID] = settings;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the output Visio file after processing
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Initialize and populate the window settings cache
                var settingsCache = new WindowSettingsCache();
                settingsCache.LoadCache(diagram);

                // Example batch processing: iterate all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // For demonstration, ensure that the first window's grid visibility
                        // matches the cached value before performing any shape-specific logic.
                        if (diagram.Windows.Count > 0)
                        {
                            Window firstWindow = diagram.Windows[0];
                            WindowSettings cached = settingsCache.GetSettings(firstWindow.ID);
                            if (cached != null && firstWindow.ShowGrid != cached.ShowGrid)
                            {
                                // Apply the cached setting to avoid redundant API calls
                                firstWindow.ShowGrid = cached.ShowGrid;
                                // Update cache to reflect the change (optional, here it's unchanged)
                                settingsCache.UpdateCache(firstWindow);
                            }
                        }

                        // Placeholder for additional shape processing logic
                        // e.g., modify shape text, style, etc.
                    }
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
}