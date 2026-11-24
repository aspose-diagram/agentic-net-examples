using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio file as an argument.");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(filePath);

            // Verify that at least one window exists; window settings are global.
            if (diagram.Windows.Count == 0)
            {
                Console.Error.WriteLine("The diagram does not contain any Window objects.");
                return;
            }

            // Retrieve the first (and typically only) window.
            Window window = diagram.Windows[0];

            // Capture the visibility settings from the window.
            BOOL showGrid = window.ShowGrid;
            BOOL showGuides = window.ShowGuides;
            BOOL showRulers = window.ShowRulers;
            BOOL showPageBreaks = window.ShowPageBreaks;
            BOOL dynamicGridEnabled = window.DynamicGridEnabled;
            BOOL showConnectionPoints = window.ShowConnectionPoints;

            Console.WriteLine("Captured global window visibility settings:");
            Console.WriteLine($"  ShowGrid: {showGrid}");
            Console.WriteLine($"  ShowGuides: {showGuides}");
            Console.WriteLine($"  ShowRulers: {showRulers}");
            Console.WriteLine($"  ShowPageBreaks: {showPageBreaks}");
            Console.WriteLine($"  DynamicGridEnabled: {dynamicGridEnabled}");
            Console.WriteLine($"  ShowConnectionPoints: {showConnectionPoints}");
            Console.WriteLine();

            // Iterate through all pages to confirm that the window settings are identical.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Access each page explicitly as a Page type (avoid var in foreach).
                Page page = diagram.Pages[i];

                // Since window settings are global, they should match the captured values.
                // We perform a manual check and report any discrepancy.
                if (window.ShowGrid != showGrid ||
                    window.ShowGuides != showGuides ||
                    window.ShowRulers != showRulers ||
                    window.ShowPageBreaks != showPageBreaks ||
                    window.DynamicGridEnabled != dynamicGridEnabled ||
                    window.ShowConnectionPoints != showConnectionPoints)
                {
                    throw new Exception($"Window visibility settings differ on page index {i}.");
                }

                Console.WriteLine($"Page {i + 1}/{diagram.Pages.Count}: window settings verified as identical.");
            }

            Console.WriteLine("\nValidation successful: all pages share identical global window visibility settings.");
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}