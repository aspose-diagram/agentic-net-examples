using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    static void Main(string[] args)
    {
        // Expect the first argument to be the path of the Visio diagram.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio diagram file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure at least one Window exists; create a default one if necessary.
            if (diagram.Windows.Count == 0)
            {
                // Create a new Window instance.
                Window defaultWindow = new Window();

                /// <summary>
                /// WindowType defines the kind of window (Drawing, Stencil, Sheet, Icon).
                /// </summary>
                defaultWindow.WindowType = WindowTypeValue.Drawing;

                /// <summary>
                /// WindowWidth sets the initial width of the window in pixels.
                /// </summary>
                defaultWindow.WindowWidth = 800;

                /// <summary>
                /// WindowHeight sets the initial height of the window in pixels.
                /// </summary>
                defaultWindow.WindowHeight = 600;

                /// <summary>
                /// WindowState determines the visual state of the window (Maximized, Minimized, or Normal via cast).
                /// </summary>
                defaultWindow.WindowState = WindowStateValue.Maximized;

                // Add the newly created window to the diagram.
                diagram.Windows.Add(defaultWindow);
            }

            // Retrieve the first window for demonstration purposes.
            Window window = diagram.Windows[0];

            /// <summary>
            /// ID is the unique identifier of the window within the diagram.
            /// </summary>
            int windowId = window.ID;

            /// <summary>
            /// WindowType indicates the category of the window (Drawing, Stencil, Sheet, Icon).
            /// </summary>
            WindowTypeValue windowType = window.WindowType;

            /// <summary>
            /// WindowWidth represents the width of the window in pixels.
            /// </summary>
            long windowWidth = window.WindowWidth;

            /// <summary>
            /// WindowHeight represents the height of the window in pixels.
            /// </summary>
            long windowHeight = window.WindowHeight;

            /// <summary>
            /// WindowState reflects the current state of the window (Maximized, Minimized, or Normal via cast).
            /// </summary>
            WindowStateValue windowState = window.WindowState;

            /// <summary>
            /// ShowGrid determines whether the grid is displayed in the window.
            /// </summary>
            BOOL showGrid = window.ShowGrid;

            /// <summary>
            /// ShowGuides determines whether alignment guides are displayed in the window.
            /// </summary>
            BOOL showGuides = window.ShowGuides;

            /// <summary>
            /// ShowRulers determines whether rulers are displayed in the window.
            /// </summary>
            BOOL showRulers = window.ShowRulers;

            /// <summary>
            /// ShowPageBreaks determines whether page break lines are displayed in the window.
            /// </summary>
            BOOL showPageBreaks = window.ShowPageBreaks;

            /// <summary>
            /// DynamicGridEnabled enables or disables the dynamic grid feature for shape placement.
            /// </summary>
            BOOL dynamicGridEnabled = window.DynamicGridEnabled;

            /// <summary>
            /// ShowConnectionPoints toggles the visibility of connection points on shapes.
            /// </summary>
            BOOL showConnectionPoints = window.ShowConnectionPoints;

            // Output the retrieved window property values.
            Console.WriteLine($"Window ID: {windowId}");
            Console.WriteLine($"Window Type: {windowType}");
            Console.WriteLine($"Size (W x H): {windowWidth} x {windowHeight}");
            Console.WriteLine($"Window State: {windowState}");
            Console.WriteLine($"Show Grid: {showGrid}");
            Console.WriteLine($"Show Guides: {showGuides}");
            Console.WriteLine($"Show Rulers: {showRulers}");
            Console.WriteLine($"Show Page Breaks: {showPageBreaks}");
            Console.WriteLine($"Dynamic Grid Enabled: {dynamicGridEnabled}");
            Console.WriteLine($"Show Connection Points: {showConnectionPoints}");

            // Example modifications to some BOOL properties.
            /// <summary>
            /// Hide the grid by setting ShowGrid to FALSE.
            /// </summary>
            window.ShowGrid = BOOL.False;

            /// <summary>
            /// Ensure guides are visible by setting ShowGuides to TRUE.
            /// </summary>
            window.ShowGuides = BOOL.True;

            /// <summary>
            /// Enable the dynamic grid feature.
            /// </summary>
            window.DynamicGridEnabled = BOOL.True;

            // Save the modified diagram to a new file.
            string outputPath = Path.Combine(
                Path.GetDirectoryName(diagramPath) ?? string.Empty,
                "Modified_" + Path.GetFileName(diagramPath));

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}