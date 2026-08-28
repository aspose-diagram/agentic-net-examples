using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Create AutoSpaceOptions and set horizontal distance to zero
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 0.0; // Align shapes vertically without horizontal gaps

            // Apply auto-spacing to all shapes on the page using the configured options
            page.AutoSpaceShapes(page.Shapes, options);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
