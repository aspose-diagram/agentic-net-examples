using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page (or any specific page)
            Page page = diagram.Pages[0];

            // Configure autospace options
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 0.5; // horizontal spacing in inches
            options.DistanceInVertical = 0.5;   // vertical spacing in inches

            try
            {
                // Auto‑space all shapes on the page using the specified options
                page.AutoSpaceShapes(page.Shapes, options);
            }
            catch (Exception ex)
            {
                // Gracefully handle any runtime errors that occur during auto‑spacing
                Console.WriteLine($"Auto‑spacing failed: {ex.Message}");
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
