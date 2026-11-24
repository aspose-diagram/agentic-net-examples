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

        string filePath = args[0];

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(filePath))
            {
                // Check if any windows are present
                if (diagram.Windows == null || diagram.Windows.Count == 0)
                {
                    Console.WriteLine("No window elements found in the diagram.");
                    return;
                }

                // Iterate through each window and output its visibility settings
                foreach (Window window in diagram.Windows)
                {
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine($"Window ID: {window.ID}");

                    // Visibility settings (BOOL properties)
                    string showGrid = window.ShowGrid == BOOL.True ? "True" : "False";
                    string showGuides = window.ShowGuides == BOOL.True ? "True" : "False";
                    string showRulers = window.ShowRulers == BOOL.True ? "True" : "False";
                    string showPageBreaks = window.ShowPageBreaks == BOOL.True ? "True" : "False";
                    string dynamicGrid = window.DynamicGridEnabled == BOOL.True ? "True" : "False";
                    string showConnPoints = window.ShowConnectionPoints == BOOL.True ? "True" : "False";

                    Console.WriteLine(" Visibility Settings:");
                    Console.WriteLine($"  ShowGrid               : {showGrid}");
                    Console.WriteLine($"  ShowGuides             : {showGuides}");
                    Console.WriteLine($"  ShowRulers             : {showRulers}");
                    Console.WriteLine($"  ShowPageBreaks         : {showPageBreaks}");
                    Console.WriteLine($"  DynamicGridEnabled     : {dynamicGrid}");
                    Console.WriteLine($"  ShowConnectionPoints   : {showConnPoints}");

                    // Determine applicable pages.
                    // The Window element does not expose a direct PageID property.
                    // According to Aspose.Diagram documentation, window visibility settings are global.
                    // Therefore, we list all pages in the diagram as the scope of applicability.
                    Console.WriteLine(" Applies To Pages:");
                    foreach (Page page in diagram.Pages)
                    {
                        Console.WriteLine($"  Page ID: {page.ID}, Name: {page.NameU}");
                    }
                }

                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Diagnostic report generation completed.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O exceptions and report them
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}