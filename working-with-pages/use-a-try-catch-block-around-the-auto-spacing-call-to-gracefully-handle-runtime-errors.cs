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

            // Create and configure autospace options
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 0.5; // horizontal spacing in inches
            options.DistanceInVertical = 0.5;   // vertical spacing in inches

            // Get the collection of shapes on the page
            ShapeCollection shapes = page.Shapes;

            // Attempt to auto‑space the shapes, handling any runtime errors gracefully
            try
            {
                page.AutoSpaceShapes(shapes, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Auto‑spacing failed: " + ex.Message);
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
