using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: DiagramWindowDiagnostics <VisioFilePath>");
            return;
        }

        string filePath = args[0];
        // Verify that the specified file exists.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the diagram. No special load options are required.
            Diagram diagram = new Diagram(filePath);

            // Build a lookup of page IDs to page names for easy reference.
            var pageLookup = new Dictionary<int, string>();
            foreach (Page page in diagram.Pages)
            {
                pageLookup[page.ID] = page.Name;
            }

            // Iterate through each window and output its visibility settings.
            Console.WriteLine("=== Window Visibility Diagnostic Report ===");
            foreach (Window window in diagram.Windows)
            {
                Console.WriteLine($"Window ID: {window.ID}");

                // Visibility settings (all are BOOL values).
                string showGrid = window.ShowGrid == BOOL.True ? "True" : "False";
                string showGuides = window.ShowGuides == BOOL.True ? "True" : "False";
                string showRulers = window.ShowRulers == BOOL.True ? "True" : "False";
                string showPageBreaks = window.ShowPageBreaks == BOOL.True ? "True" : "False";
                string showConnectionPoints = window.ShowConnectionPoints == BOOL.True ? "True" : "False";
                string dynamicGridEnabled = window.DynamicGridEnabled == BOOL.True ? "True" : "False";

                Console.WriteLine($"  ShowGrid: {showGrid}");
                Console.WriteLine($"  ShowGuides: {showGuides}");
                Console.WriteLine($"  ShowRulers: {showRulers}");
                Console.WriteLine($"  ShowPageBreaks: {showPageBreaks}");
                Console.WriteLine($"  ShowConnectionPoints: {showConnectionPoints}");
                Console.WriteLine($"  DynamicGridEnabled: {dynamicGridEnabled}");

                // Determine which page (if any) this window is associated with.
                // Window.Page returns a Page object; use its ID for lookup.
                if (window.Page != null && pageLookup.TryGetValue(window.Page.ID, out string pageName))
                {
                    Console.WriteLine($"  Associated Page ID: {window.Page.ID} (Name: {pageName})");
                }
                else
                {
                    Console.WriteLine("  Associated Page: None or unknown");
                }

                Console.WriteLine(); // Blank line for readability.
            }

            Console.WriteLine("=== End of Report ===");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}