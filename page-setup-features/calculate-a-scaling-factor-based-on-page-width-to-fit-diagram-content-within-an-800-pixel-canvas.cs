using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Retrieve the page width in inches
                double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;

                // Define the target canvas width in pixels
                const double targetCanvasPixels = 800.0;

                // Define the DPI (dots per inch) for the canvas.
                // 96 DPI is a common screen resolution.
                const double dpi = 96.0;

                // Convert target canvas width to inches
                double targetCanvasInches = targetCanvasPixels / dpi;

                // Calculate scaling factor to fit the diagram width into the canvas
                double scalingFactor = targetCanvasInches / pageWidthInches;

                // Output the result
                Console.WriteLine($"Page width (inches): {pageWidthInches}");
                Console.WriteLine($"Target canvas width (pixels): {targetCanvasPixels}");
                Console.WriteLine($"Scaling factor to fit within 800‑pixel canvas: {scalingFactor}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }