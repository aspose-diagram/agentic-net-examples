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

            // Access the first page (or any specific page)
            Page page = diagram.Pages[0];

            // Create AutoSpaceOptions and set custom distances (in inches)
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 1.0; // 1 inch horizontal spacing
            options.DistanceInVertical = 0.5;   // 0.5 inch vertical spacing

            // Apply auto‑spacing to all shapes on the page
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
