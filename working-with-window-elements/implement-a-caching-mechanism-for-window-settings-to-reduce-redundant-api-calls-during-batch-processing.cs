using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace DiagramWindowCacheDemo
{
    // DTO to hold window settings that are frequently accessed
    public class WindowSettings
    {
        public BOOL ShowGrid { get; set; }
        public BOOL ShowGuides { get; set; }
        public BOOL ShowRulers { get; set; }
        public BOOL ShowPageBreaks { get; set; }
        public BOOL DynamicGridEnabled { get; set; }
        public BOOL ShowConnectionPoints { get; set; }

        // Apply cached settings back to a Window instance
        public void ApplyTo(Window window)
        {
            window.ShowGrid = ShowGrid;
            window.ShowGuides = ShowGuides;
            window.ShowRulers = ShowRulers;
            window.ShowPageBreaks = ShowPageBreaks;
            window.DynamicGridEnabled = DynamicGridEnabled;
            window.ShowConnectionPoints = ShowConnectionPoints;
        }

        // Create a copy from an existing Window
        public static WindowSettings FromWindow(Window window)
        {
            return new WindowSettings
            {
                ShowGrid = window.ShowGrid,
                ShowGuides = window.ShowGuides,
                ShowRulers = window.ShowRulers,
                ShowPageBreaks = window.ShowPageBreaks,
                DynamicGridEnabled = window.DynamicGridEnabled,
                ShowConnectionPoints = window.ShowConnectionPoints
            };
        }
    }

    // Simple cache for window settings keyed by Window.ID
    public class WindowSettingsCache
    {
        private readonly Diagram _diagram;
        private readonly Dictionary<int, WindowSettings> _cache = new();

        public WindowSettingsCache(Diagram diagram)
        {
            _diagram = diagram ?? throw new ArgumentNullException(nameof(diagram));
        }

        // Retrieve settings; if not cached, read from the diagram and store
        public WindowSettings GetSettings(int windowId)
        {
            if (_cache.TryGetValue(windowId, out var settings))
                return settings;

            // Find the window with the specified ID
            Window targetWindow = null;
            foreach (Window w in _diagram.Windows)
            {
                if (w.ID == windowId)
                {
                    targetWindow = w;
                    break;
                }
            }

            if (targetWindow == null)
                throw new InvalidOperationException($"Window with ID {windowId} not found.");

            // Cache the settings
            settings = WindowSettings.FromWindow(targetWindow);
            _cache[windowId] = settings;
            return settings;
        }

        // Update cache after modifying a window
        public void UpdateCache(int windowId, WindowSettings newSettings)
        {
            if (newSettings == null)
                throw new ArgumentNullException(nameof(newSettings));

            _cache[windowId] = newSettings;
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing diagram (replace with actual path)
                var diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Ensure at least one window exists; otherwise add a default one
                if (diagram.Windows.Count == 0)
                {
                    var defaultWindow = new Window
                    {
                        WindowState = WindowStateValue.Maximized,
                        WindowWidth = 1100,
                        WindowHeight = 700,
                        WindowType = WindowTypeValue.Drawing,
                        ShowGrid = BOOL.True,
                        ShowGuides = BOOL.True,
                        ShowRulers = BOOL.True,
                        ShowPageBreaks = BOOL.False,
                        DynamicGridEnabled = BOOL.False,
                        ShowConnectionPoints = BOOL.False
                    };
                    diagram.Windows.Add(defaultWindow);
                }

                // Initialize the cache
                var cache = new WindowSettingsCache(diagram);

                // Example batch processing: iterate all pages and ensure each window has the same settings
                foreach (Page page in diagram.Pages)
                {
                    // For demonstration, use the first window's ID
                    int windowId = diagram.Windows[0].ID;

                    // Retrieve cached settings (avoids repeated property reads)
                    WindowSettings settings = cache.GetSettings(windowId);

                    // Suppose we want to enable the grid for all windows during processing
                    if (settings.ShowGrid != BOOL.True)
                    {
                        settings.ShowGrid = BOOL.True;
                        // Apply the modified settings back to the actual window
                        Window targetWindow = diagram.Windows[0];
                        settings.ApplyTo(targetWindow);
                        // Update the cache with the new values
                        cache.UpdateCache(windowId, settings);
                    }

                    // Additional per-page logic could go here...
                    Console.WriteLine($"Processed page '{page.Name}' with window ID {windowId}.");
                }

                // Save the diagram after batch modifications
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}