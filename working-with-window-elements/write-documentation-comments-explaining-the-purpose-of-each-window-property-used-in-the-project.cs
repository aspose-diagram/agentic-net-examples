using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

/// <summary>
    /// Demonstrates accessing and configuring Window properties in an Aspose.Diagram document.
    /// Each property is documented with its purpose.
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Create an empty diagram. New diagrams have no windows by default.
            Diagram diagram = new Diagram();

            // Ensure at least one window exists before accessing the collection.
            if (diagram.Windows.Count == 0)
            {
                // Create a new window and add it to the diagram.
                Window initialWindow = new Window();

                // WindowTypeValue.Drawing – indicates the window displays a drawing (the main canvas).
                initialWindow.WindowType = WindowTypeValue.Drawing;

                // WindowStateValue.Maximized – opens the window maximized on the screen.
                initialWindow.WindowState = WindowStateValue.Maximized;

                // Set default size for the window (in internal units).
                // WindowHeight – height of the window.
                initialWindow.WindowHeight = 800;
                // WindowWidth – width of the window.
                initialWindow.WindowWidth = 1200;

                diagram.Windows.Add(initialWindow);
            }

            // Iterate through all windows in the diagram.
            foreach (Window window in diagram.Windows)
            {
                // ID – unique identifier of the window within the document.
                Console.WriteLine($"Window ID: {window.ID}");

                // WindowType – defines the kind of window (Drawing, Stencil, Sheet, Icon).
                Console.WriteLine($"Window Type: {window.WindowType}");

                // WindowState – current visual state of the window (Maximized, Minimized).
                Console.WriteLine($"Window State: {window.WindowState}");

                // WindowHeight – height of the window in internal units.
                Console.WriteLine($"Window Height: {window.WindowHeight}");

                // WindowWidth – width of the window in internal units.
                Console.WriteLine($"Window Width: {window.WindowWidth}");

                // DynamicGridEnabled – when TRUE, the dynamic grid feature is active, helping with shape alignment.
                Console.WriteLine($"Dynamic Grid Enabled: {window.DynamicGridEnabled}");

                // ShowConnectionPoints – when TRUE, connection points on shapes are displayed.
                Console.WriteLine($"Show Connection Points: {window.ShowConnectionPoints}");

                // ShowGrid – when TRUE, the drawing grid is visible.
                Console.WriteLine($"Show Grid: {window.ShowGrid}");

                // ShowGuides – when TRUE, guide lines are displayed to assist alignment.
                Console.WriteLine($"Show Guides: {window.ShowGuides}");

                // ShowRulers – when TRUE, rulers are displayed along the top and left edges of the window.
                Console.WriteLine($"Show Rulers: {window.ShowRulers}");

                // ShowPageBreaks – when TRUE, page break indicators are shown.
                Console.WriteLine($"Show Page Breaks: {window.ShowPageBreaks}");

                Console.WriteLine(new string('-', 40));
            }

            // Example: modify properties of the first window.
            Window firstWindow = diagram.Windows[0];

            // Enable dynamic grid.
            // DynamicGridEnabled accepts BOOL values, not plain bool.
            firstWindow.DynamicGridEnabled = BOOL.True;

            // Show connection points.
            firstWindow.ShowConnectionPoints = BOOL.True;

            // Show grid, guides, rulers, and page breaks.
            firstWindow.ShowGrid = BOOL.True;
            firstWindow.ShowGuides = BOOL.True;
            firstWindow.ShowRulers = BOOL.True;
            firstWindow.ShowPageBreaks = BOOL.True;

            // Change window size.
            firstWindow.WindowHeight = 900;
            firstWindow.WindowWidth = 1300;

            // Save the diagram to a VDX file (Visio 2003 format) as an example.
            diagram.Save("output.vdx", SaveFileFormat.Vdx);
        }
    }