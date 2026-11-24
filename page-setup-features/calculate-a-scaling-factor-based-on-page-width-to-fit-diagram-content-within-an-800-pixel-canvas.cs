using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: Diagram contains no pages.");
                    return;
                }

                // Retrieve the first (foreground) page.
                Page page = diagram.Pages[0];

                // Get the page width in inches via the PageProps object.
                double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;

                // Guard against invalid width values.
                if (pageWidthInches <= 0)
                {
                    Console.Error.WriteLine("Error: Invalid page width retrieved from diagram.");
                    return;
                }

                // Assume a standard screen DPI of 96 dots per inch.
                const double dpi = 96.0;

                // Convert page width to pixels.
                double pageWidthPixels = pageWidthInches * dpi;

                // Desired canvas width in pixels.
                const double canvasWidthPixels = 800.0;

                // Calculate scaling factor to fit the diagram within the canvas.
                double scalingFactor = canvasWidthPixels / pageWidthPixels;

                // Output the result.
                Console.WriteLine($"Page width (inches): {pageWidthInches}");
                Console.WriteLine($"Page width (pixels) at {dpi} DPI: {pageWidthPixels}");
                Console.WriteLine($"Scaling factor to fit within {canvasWidthPixels}px: {scalingFactor}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}