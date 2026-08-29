using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one window; if none, create a default one
                if (diagram.Windows.Count == 0)
                {
                    Window defaultWindow = new Window();
                    defaultWindow.WindowState = WindowStateValue.Maximized;
                    defaultWindow.WindowHeight = 500;
                    defaultWindow.WindowWidth = 500;
                    defaultWindow.WindowType = WindowTypeValue.Drawing;
                    diagram.Windows.Add(defaultWindow);
                }

                // Iterate through each window and toggle visibility properties,
                // logging the before and after states.
                foreach (Window window in diagram.Windows)
                {
                    Console.WriteLine($"--- Window ID: {window.ID} ---");

                    // ShowGrid
                    Console.WriteLine($"ShowGrid before: {window.ShowGrid}");
                    window.ShowGrid = (window.ShowGrid == BOOL.True) ? BOOL.False : BOOL.True;
                    Console.WriteLine($"ShowGrid after : {window.ShowGrid}");

                    // ShowGuides
                    Console.WriteLine($"ShowGuides before: {window.ShowGuides}");
                    window.ShowGuides = (window.ShowGuides == BOOL.True) ? BOOL.False : BOOL.True;
                    Console.WriteLine($"ShowGuides after : {window.ShowGuides}");

                    // ShowRulers
                    Console.WriteLine($"ShowRulers before: {window.ShowRulers}");
                    window.ShowRulers = (window.ShowRulers == BOOL.True) ? BOOL.False : BOOL.True;
                    Console.WriteLine($"ShowRulers after : {window.ShowRulers}");

                    // ShowPageBreaks
                    Console.WriteLine($"ShowPageBreaks before: {window.ShowPageBreaks}");
                    window.ShowPageBreaks = (window.ShowPageBreaks == BOOL.True) ? BOOL.False : BOOL.True;
                    Console.WriteLine($"ShowPageBreaks after : {window.ShowPageBreaks}");

                    // ShowConnectionPoints
                    Console.WriteLine($"ShowConnectionPoints before: {window.ShowConnectionPoints}");
                    window.ShowConnectionPoints = (window.ShowConnectionPoints == BOOL.True) ? BOOL.False : BOOL.True;
                    Console.WriteLine($"ShowConnectionPoints after : {window.ShowConnectionPoints}");

                    // DynamicGridEnabled
                    Console.WriteLine($"DynamicGridEnabled before: {window.DynamicGridEnabled}");
                    window.DynamicGridEnabled = (window.DynamicGridEnabled == BOOL.True) ? BOOL.False : BOOL.True;
                    Console.WriteLine($"DynamicGridEnabled after : {window.DynamicGridEnabled}");

                    Console.WriteLine(); // Blank line for readability
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