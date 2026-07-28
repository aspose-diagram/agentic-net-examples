using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page for calculation
                Page page = diagram.Pages[0];

                // Page width is stored in inches
                double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;

                // Assume a standard screen DPI (dots per inch)
                const double dpi = 96.0;

                // Convert page width to pixels
                double pageWidthPixels = pageWidthInches * dpi;

                // Desired canvas width in pixels
                const double canvasWidthPixels = 800.0;

                // Calculate scaling factor to fit the page within the canvas
                double scalingFactor = canvasWidthPixels / pageWidthPixels;

                Console.WriteLine($"Page width (inches): {pageWidthInches}");
                Console.WriteLine($"Page width (pixels) at {dpi} DPI: {pageWidthPixels}");
                Console.WriteLine($"Scaling factor to fit within {canvasWidthPixels}px canvas: {scalingFactor}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }