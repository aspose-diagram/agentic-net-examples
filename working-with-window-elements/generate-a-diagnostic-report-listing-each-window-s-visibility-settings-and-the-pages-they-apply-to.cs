using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file – adjust as needed.
        string diagramPath = "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Load the diagram.
        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Ensure there is at least one window.
        if (diagram.Windows.Count == 0)
        {
            Console.Error.WriteLine("No windows found in the diagram.");
            return;
        }

        // Iterate through each window and report its visibility settings.
        foreach (Window window in diagram.Windows)
        {
            Console.WriteLine($"Window ID: {window.ID}");
            Console.WriteLine($"  Type: {window.WindowType}");

            // Visibility settings – each property is of type BOOL.
            Console.WriteLine($"  ShowGrid: {(window.ShowGrid == BOOL.True ? "True" : "False")}");
            Console.WriteLine($"  ShowGuides: {(window.ShowGuides == BOOL.True ? "True" : "False")}");
            Console.WriteLine($"  ShowRulers: {(window.ShowRulers == BOOL.True ? "True" : "False")}");
            Console.WriteLine($"  ShowPageBreaks: {(window.ShowPageBreaks == BOOL.True ? "True" : "False")}");
            Console.WriteLine($"  ShowConnectionPoints: {(window.ShowConnectionPoints == BOOL.True ? "True" : "False")}");
            Console.WriteLine($"  DynamicGridEnabled: {(window.DynamicGridEnabled == BOOL.True ? "True" : "False")}");

            // Determine the page(s) the window applies to, if applicable.
            // For drawing windows the Page property holds a reference to the page.
            if (window.Page != null)
            {
                try
                {
                    Page page = window.Page;
                    Console.WriteLine($"  Applies to Page ID: {page.ID}, Name: {page.Name}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"  Error retrieving page for window ID {window.ID}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("  Applies to: All pages (global window settings)");
            }

            Console.WriteLine(); // Blank line between windows.
        }
    }
}