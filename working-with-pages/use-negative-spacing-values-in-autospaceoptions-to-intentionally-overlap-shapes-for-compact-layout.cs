using System.IO;
using System;
using Aspose.Diagram;

using Aspose.Diagram.AutoLayout; // Namespace for AutoSpaceOptions

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

            // Prepare AutoSpaceOptions with negative spacing to force overlap
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                // Negative values (in inches) cause shapes to be placed closer together,
                // resulting in intentional overlap.
                DistanceInHorizontal = -0.2, // overlap horizontally by 0.2 inch
                DistanceInVertical = -0.2    // overlap vertically by 0.2 inch
            };

            // Apply auto‑spacing to all shapes on the page using the options above
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
