using System;
using Aspose.Diagram;

/// <summary>
    /// Demonstrates accessing Window properties and documents their purpose via XML comments.
    /// </summary>
    class Program
    {
        static void Main()
        {
            try
            {

                // Load a Visio diagram from file.
                Diagram diagram = new Diagram("sample.vsdx");

                // Iterate through each window in the diagram.
                foreach (Window window in diagram.Windows)
                {
                    /// <summary>
                    /// ID: The unique identifier of the window within its parent element.
                    /// </summary>
                    int windowId = window.ID;

                    /// <summary>
                    /// WindowType: Specifies the kind of window (Drawing, Sheet, Stencil, or Icon).
                    /// </summary>
                    var type = window.WindowType;

                    /// <summary>
                    /// WindowState: Indicates the current state of the window (Normal, Minimized, Maximized).
                    /// </summary>
                    var state = window.WindowState;

                    /// <summary>
                    /// WindowHeight: Height of the window rectangle.
                    /// </summary>
                    long height = window.WindowHeight;

                    /// <summary>
                    /// WindowWidth: Width of the window rectangle.
                    /// </summary>
                    long width = window.WindowWidth;

                    /// <summary>
                    /// DynamicGridEnabled: Specifies whether the dynamic grid feature is enabled for this window.
                    /// </summary>
                    BOOL dynamicGrid = window.DynamicGridEnabled;

                    /// <summary>
                    /// ShowConnectionPoints: Determines whether connection points are displayed in the window.
                    /// </summary>
                    BOOL showConnPoints = window.ShowConnectionPoints;

                    /// <summary>
                    /// ShowGrid: Determines whether the drawing grid is visible in the window.
                    /// </summary>
                    BOOL showGrid = window.ShowGrid;

                    /// <summary>
                    /// ShowGuides: Determines whether guides are visible in the window.
                    /// </summary>
                    BOOL showGuides = window.ShowGuides;

                    /// <summary>
                    /// ShowRulers: Determines whether rulers are visible in the window.
                    /// </summary>
                    BOOL showRulers = window.ShowRulers;

                    /// <summary>
                    /// ShowPageBreaks: Determines whether page breaks are visible in the window.
                    /// </summary>
                    BOOL showPageBreaks = window.ShowPageBreaks;

                    // Output the collected information.
                    Console.WriteLine($"Window ID: {windowId}");
                    Console.WriteLine($"Type: {type}");
                    Console.WriteLine($"State: {state}");
                    Console.WriteLine($"Size: {width}x{height}");
                    Console.WriteLine($"Dynamic Grid: {dynamicGrid}");
                    Console.WriteLine($"Show Connection Points: {showConnPoints}");
                    Console.WriteLine($"Show Grid: {showGrid}");
                    Console.WriteLine($"Show Guides: {showGuides}");
                    Console.WriteLine($"Show Rulers: {showRulers}");
                    Console.WriteLine($"Show Page Breaks: {showPageBreaks}");
                    Console.WriteLine(new string('-', 40));
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }