using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// Expects a Visio file path as the first command‑line argument.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio file as the first argument.");
            return;
        }

        string filePath = args[0];

        // Guard against missing file.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(filePath);

            // Ensure at least one window exists; add a default one if necessary.
            if (diagram.Windows.Count == 0)
            {
                Console.WriteLine("No windows found in the diagram. Adding a default window.");
                Window defaultWindow = new Window
                {
                    // WindowType specifies the kind of window (Drawing, Stencil, etc.).
                    WindowType = WindowTypeValue.Drawing,
                    // WindowState defines the initial state (Maximized, Minimized, Normal).
                    WindowState = WindowStateValue.Maximized,
                    // WindowWidth and WindowHeight set the size of the window in internal units.
                    WindowWidth = 800,
                    WindowHeight = 600
                };
                diagram.Windows.Add(defaultWindow);
            }

            // Process each window in the diagram.
            for (int i = 0; i < diagram.Windows.Count; i++)
            {
                Window window = diagram.Windows[i];

                // ID: Unique identifier of the window within the diagram.
                Console.WriteLine($"Window ID: {window.ID}");

                // WindowType: Indicates the type of the window (e.g., Drawing, Stencil, Sheet, Icon).
                Console.WriteLine($"Window Type: {window.WindowType}");

                // WindowWidth: Width of the window measured in internal units.
                Console.WriteLine($"Window Width: {window.WindowWidth}");

                // WindowHeight: Height of the window measured in internal units.
                Console.WriteLine($"Window Height: {window.WindowHeight}");

                // WindowState: Current visual state of the window (Normal, Minimized, Maximized).
                Console.WriteLine($"Window State: {window.WindowState}");

                // ShowGrid: Controls whether the grid is visible in the window.
                Console.WriteLine($"Show Grid: {window.ShowGrid}");

                // ShowGuides: Controls the visibility of alignment guides.
                Console.WriteLine($"Show Guides: {window.ShowGuides}");

                // ShowRulers: Controls whether rulers are displayed.
                Console.WriteLine($"Show Rulers: {window.ShowRulers}");

                // ShowPageBreaks: Determines if page break indicators are shown.
                Console.WriteLine($"Show Page Breaks: {window.ShowPageBreaks}");

                // ShowConnectionPoints: Determines whether connection points are displayed.
                Console.WriteLine($"Show Connection Points: {window.ShowConnectionPoints}");

                // DynamicGridEnabled: Enables or disables the dynamic grid feature for easier shape placement.
                Console.WriteLine($"Dynamic Grid Enabled: {window.DynamicGridEnabled}");

                // Example modifications: enable grid, guides, rulers, connection points, and dynamic grid.
                // Use BOOL.True / BOOL.False as required by the Aspose API.
                window.ShowGrid = BOOL.True;                     // Show the grid.
                window.ShowGuides = BOOL.True;                   // Show alignment guides.
                window.ShowRulers = BOOL.True;                   // Show rulers.
                window.ShowPageBreaks = BOOL.False;              // Hide page break markers.
                window.ShowConnectionPoints = BOOL.True;         // Show connection points.
                window.DynamicGridEnabled = BOOL.True;           // Enable dynamic grid.
            }

            // Save the modified diagram next to the original file with a "_modified" suffix.
            string outputPath = Path.Combine(
                Path.GetDirectoryName(filePath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(filePath) + "_modified.vsdx");

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}