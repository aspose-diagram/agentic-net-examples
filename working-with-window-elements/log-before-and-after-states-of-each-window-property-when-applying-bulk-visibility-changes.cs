using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one window; if not, create a default drawing window
                if (diagram.Windows.Count == 0)
                {
                    Window defaultWindow = new Window
                    {
                        WindowType = WindowTypeValue.Drawing,
                        WindowState = WindowStateValue.Maximized,
                        WindowWidth = 1100,
                        WindowHeight = 700
                    };
                    diagram.Windows.Add(defaultWindow);
                }

                // Iterate through each window and log before/after states of visibility-related properties
                foreach (Window window in diagram.Windows)
                {
                    // Log before state
                    Console.WriteLine($"Window ID: {window.ID}");
                    Console.WriteLine($"  Before - ShowGrid: {window.ShowGrid}");
                    Console.WriteLine($"  Before - ShowGuides: {window.ShowGuides}");
                    Console.WriteLine($"  Before - ShowRulers: {window.ShowRulers}");
                    Console.WriteLine($"  Before - ShowPageBreaks: {window.ShowPageBreaks}");
                    Console.WriteLine($"  Before - ShowConnectionPoints: {window.ShowConnectionPoints}");
                    Console.WriteLine($"  Before - DynamicGridEnabled: {window.DynamicGridEnabled}");

                    // Apply bulk visibility changes (hide all)
                    window.ShowGrid = BOOL.False;
                    window.ShowGuides = BOOL.False;
                    window.ShowRulers = BOOL.False;
                    window.ShowPageBreaks = BOOL.False;
                    window.ShowConnectionPoints = BOOL.False;
                    window.DynamicGridEnabled = BOOL.False;

                    // Log after state
                    Console.WriteLine($"  After  - ShowGrid: {window.ShowGrid}");
                    Console.WriteLine($"  After  - ShowGuides: {window.ShowGuides}");
                    Console.WriteLine($"  After  - ShowRulers: {window.ShowRulers}");
                    Console.WriteLine($"  After  - ShowPageBreaks: {window.ShowPageBreaks}");
                    Console.WriteLine($"  After  - ShowConnectionPoints: {window.ShowConnectionPoints}");
                    Console.WriteLine($"  After  - DynamicGridEnabled: {window.DynamicGridEnabled}");
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