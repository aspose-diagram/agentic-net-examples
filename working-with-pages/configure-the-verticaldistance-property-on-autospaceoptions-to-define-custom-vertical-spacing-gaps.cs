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

            // Create AutoSpaceOptions and set a custom vertical spacing (in inches)
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInVertical = 1.0; // Example: 1 inch vertical gap

            // Get the target page (here we use the first page)
            Page page = diagram.Pages[0];

            // Apply auto‑spacing to all shapes on the page using the custom options
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
