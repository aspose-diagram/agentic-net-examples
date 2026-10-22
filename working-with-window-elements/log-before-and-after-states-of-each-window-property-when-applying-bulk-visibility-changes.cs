using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one window; if none, add a default window
            if (diagram.Windows.Count == 0)
            {
                Window defaultWindow = new Window
                {
                    // Set basic window properties
                    WindowType = WindowTypeValue.Drawing,
                    WindowState = (WindowStateValue)0, // Normal state
                    WindowWidth = 1100,
                    WindowHeight = 700
                };
                diagram.Windows.Add(defaultWindow);
            }

            // Iterate over each window and apply bulk visibility changes
            foreach (Window window in diagram.Windows)
            {
                Console.WriteLine($"--- Window ID: {window.ID} ---");

                // ShowGrid
                BOOL beforeShowGrid = window.ShowGrid;
                Console.WriteLine($"ShowGrid before: {beforeShowGrid}");
                window.ShowGrid = BOOL.True;
                Console.WriteLine($"ShowGrid after: {window.ShowGrid}");

                // ShowGuides
                BOOL beforeShowGuides = window.ShowGuides;
                Console.WriteLine($"ShowGuides before: {beforeShowGuides}");
                window.ShowGuides = BOOL.True;
                Console.WriteLine($"ShowGuides after: {window.ShowGuides}");

                // ShowRulers
                BOOL beforeShowRulers = window.ShowRulers;
                Console.WriteLine($"ShowRulers before: {beforeShowRulers}");
                window.ShowRulers = BOOL.True;
                Console.WriteLine($"ShowRulers after: {window.ShowRulers}");

                // ShowPageBreaks
                BOOL beforeShowPageBreaks = window.ShowPageBreaks;
                Console.WriteLine($"ShowPageBreaks before: {beforeShowPageBreaks}");
                window.ShowPageBreaks = BOOL.True;
                Console.WriteLine($"ShowPageBreaks after: {window.ShowPageBreaks}");

                // DynamicGridEnabled
                BOOL beforeDynamicGrid = window.DynamicGridEnabled;
                Console.WriteLine($"DynamicGridEnabled before: {beforeDynamicGrid}");
                window.DynamicGridEnabled = BOOL.True;
                Console.WriteLine($"DynamicGridEnabled after: {window.DynamicGridEnabled}");

                // ShowConnectionPoints
                BOOL beforeConnectionPoints = window.ShowConnectionPoints;
                Console.WriteLine($"ShowConnectionPoints before: {beforeConnectionPoints}");
                window.ShowConnectionPoints = BOOL.True;
                Console.WriteLine($"ShowConnectionPoints after: {window.ShowConnectionPoints}");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}