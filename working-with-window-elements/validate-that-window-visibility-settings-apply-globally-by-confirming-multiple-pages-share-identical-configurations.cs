using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Guard: ensure at least one window exists
            if (diagram.Windows.Count == 0)
            {
                Console.Error.WriteLine("The diagram does not contain any windows to evaluate.");
                return;
            }

            // Capture the visibility settings from the first window (global reference)
            Window referenceWindow = diagram.Windows[0];
            BOOL refShowGrid = referenceWindow.ShowGrid;
            BOOL refShowGuides = referenceWindow.ShowGuides;
            BOOL refShowRulers = referenceWindow.ShowRulers;
            BOOL refShowPageBreaks = referenceWindow.ShowPageBreaks;
            BOOL refDynamicGridEnabled = referenceWindow.DynamicGridEnabled;
            BOOL refShowConnectionPoints = referenceWindow.ShowConnectionPoints;

            // Iterate over all windows and verify they share the same settings
            for (int i = 1; i < diagram.Windows.Count; i++)
            {
                Window win = diagram.Windows[i];

                if (win.ShowGrid != refShowGrid ||
                    win.ShowGuides != refShowGuides ||
                    win.ShowRulers != refShowRulers ||
                    win.ShowPageBreaks != refShowPageBreaks ||
                    win.DynamicGridEnabled != refDynamicGridEnabled ||
                    win.ShowConnectionPoints != refShowConnectionPoints)
                {
                    Console.Error.WriteLine($"Window at index {i} has differing visibility settings.");
                    throw new Exception("Window visibility settings are not consistent across all windows.");
                }
            }

            // If we reach this point, all windows share identical configurations
            Console.WriteLine("Validation successful: All windows share identical visibility settings.");
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}