using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <diagram-file-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            // Retrieve the first (foreground) page.
            Page page = diagram.Pages[0];

            // Fetch the page width in inches via the correct property path.
            double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;

            // Define the DPI conversion factor (standard screen DPI).
            const double dpi = 96.0;

            // Convert page width from inches to pixels.
            double pageWidthPixels = pageWidthInches * dpi;

            // Desired canvas width in pixels.
            const double canvasWidthPixels = 800.0;

            // Calculate the scaling factor needed to fit the page width into the canvas.
            double scalingFactor = canvasWidthPixels / pageWidthPixels;

            // Output the result.
            Console.WriteLine($"Scaling factor to fit page width into an {canvasWidthPixels}px canvas: {scalingFactor}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}