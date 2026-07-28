using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect a Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: DiagramWindowDiagnostics <visio-file-path>");
            return;
        }

        string filePath = args[0];
        // Verify the input file exists before proceeding.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(filePath);

            // Iterate through each window in the document.
            foreach (Window window in diagram.Windows)
            {
                // Retrieve visibility settings (BOOL values) and convert to readable strings.
                string showGrid = window.ShowGrid == BOOL.True ? "True" : "False";
                string showGuides = window.ShowGuides == BOOL.True ? "True" : "False";
                string showRulers = window.ShowRulers == BOOL.True ? "True" : "False";
                string showPageBreaks = window.ShowPageBreaks == BOOL.True ? "True" : "False";
                string showConnectionPoints = window.ShowConnectionPoints == BOOL.True ? "True" : "False";
                string dynamicGridEnabled = window.DynamicGridEnabled == BOOL.True ? "True" : "False";

                // Determine the page the window applies to (if any).
                string pageInfo = "N/A";
                // The Window.Page property returns a Page object (or null) rather than an integer.
                if (window.Page != null)
                {
                    Page page = window.Page;
                    pageInfo = $"{page.Name} (ID {page.ID})";
                }

                // Output the diagnostic information for the current window.
                Console.WriteLine($"Window ID: {window.ID}");
                Console.WriteLine($"  Type: {window.WindowType}");
                Console.WriteLine($"  ShowGrid: {showGrid}");
                Console.WriteLine($"  ShowGuides: {showGuides}");
                Console.WriteLine($"  ShowRulers: {showRulers}");
                Console.WriteLine($"  ShowPageBreaks: {showPageBreaks}");
                Console.WriteLine($"  ShowConnectionPoints: {showConnectionPoints}");
                Console.WriteLine($"  DynamicGridEnabled: {dynamicGridEnabled}");
                Console.WriteLine($"  Applies to Page: {pageInfo}");
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}