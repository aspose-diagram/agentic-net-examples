using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (or any specific page)
            Page page = diagram.Pages[0];

            // Create AutoSpaceOptions and set a custom horizontal spacing (in inches)
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 1.0; // Example: 1 inch gap between shapes horizontally

            // Apply auto‑spacing to all shapes on the page using the configured options
            page.AutoSpaceShapes(page.Shapes, options);

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
