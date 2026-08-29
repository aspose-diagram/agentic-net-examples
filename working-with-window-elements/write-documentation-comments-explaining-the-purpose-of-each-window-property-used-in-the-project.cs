using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// Loads a Visio diagram, ensures a window exists, and prints documentation for each Window property.
    /// </summary>
    /// <param name="args">Command‑line arguments; expects the first argument to be the diagram file path.</param>
    static void Main(string[] args)
    {
        // Validate that a file path argument was provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: program <diagram-file-path>");
            return;
        }

        // Assign the first argument to a variable and guard its existence.
        string diagramPath = args[0];
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram contains at least one Window; if not, create a default one.
            if (diagram.Windows.Count == 0)
            {
                // Create a new Window with typical default settings.
                Window defaultWindow = new Window
                {
                    // Set the window type to a drawing window.
                    WindowType = WindowTypeValue.Drawing,
                    // Set the window state to maximized for visibility.
                    WindowState = WindowStateValue.Maximized,
                    // Define a reasonable default size.
                    WindowWidth = 1100,
                    WindowHeight = 700
                };
                diagram.Windows.Add(defaultWindow);
            }

            // Iterate over all windows in the diagram and output their documented properties.
            foreach (Window window in diagram.Windows)
            {
                PrintWindowInfo(window);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Prints detailed information about a <see cref="Window"/> instance.
    /// Each property is accompanied by a comment describing its purpose within a Visio document.
    /// </summary>
    /// <param name="window">The Window object whose properties are to be displayed.</param>
    static void PrintWindowInfo(Window window)
    {
        // Unique identifier of the window within the document.
        Console.WriteLine($"ID: {window.ID}");

        // Determines the kind of UI element the window represents (e.g., Drawing, Stencil).
        Console.WriteLine($"WindowType: {window.WindowType}");

        // Height of the window in internal units (typically points).
        Console.WriteLine($"WindowHeight: {window.WindowHeight}");

        // Width of the window in internal units (typically points).
        Console.WriteLine($"WindowWidth: {window.WindowWidth}");

        // Current visual state of the window (Maximized, Minimized, or default).
        Console.WriteLine($"WindowState: {window.WindowState}");

        // Enables or disables the dynamic grid feature for this window.
        Console.WriteLine($"DynamicGridEnabled: {window.DynamicGridEnabled}");

        // Controls whether connection points are shown when a shape is selected.
        Console.WriteLine($"ShowConnectionPoints: {window.ShowConnectionPoints}");

        // Toggles the visibility of the grid background in the window.
        Console.WriteLine($"ShowGrid: {window.ShowGrid}");

        // Toggles the visibility of guide lines in the window.
        Console.WriteLine($"ShowGuides: {window.ShowGuides}");

        // Toggles the visibility of rulers along the top and left edges of the window.
        Console.WriteLine($"ShowRulers: {window.ShowRulers}");

        // Determines whether page break indicators are displayed in the window.
        Console.WriteLine($"ShowPageBreaks: {window.ShowPageBreaks}");

        // Separator for readability between windows.
        Console.WriteLine(new string('-', 40));
    }
}