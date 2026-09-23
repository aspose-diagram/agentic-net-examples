using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string diagramPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Assume we work with the first page
            Page page = diagram.Pages[0];

            // Page width is stored in inches
            double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;

            // Define the target canvas width in pixels
            const double canvasWidthPixels = 800.0;

            // Typical screen DPI (dots per inch). Adjust if a different DPI is required.
            const double dpi = 96.0;

            // Calculate the scaling factor needed to fit the page width into the canvas width
            double scalingFactor = canvasWidthPixels / (pageWidthInches * dpi);

            // Output the result
            Console.WriteLine($"Page width (inches): {pageWidthInches}");
            Console.WriteLine($"Scaling factor to fit {canvasWidthPixels}px canvas: {scalingFactor}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
