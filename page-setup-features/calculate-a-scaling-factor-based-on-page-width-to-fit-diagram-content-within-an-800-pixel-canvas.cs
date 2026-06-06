using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page
            Page page = diagram.Pages[0];

            // Retrieve the page width in inches
            double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;

            // Define the DPI (dots per inch) used for pixel conversion; 96 DPI is common for screens
            const double dpi = 96.0;

            // Calculate the scaling factor to fit the page width into an 800‑pixel canvas
            double scalingFactor = 800.0 / (pageWidthInches * dpi);

            // Output the result
            Console.WriteLine($"Page width (inches): {pageWidthInches}");
            Console.WriteLine($"Scaling factor to fit 800px canvas: {scalingFactor}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
