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

            // Get the first page (or any specific page you want to modify)
            Page page = diagram.Pages[0];

            // Create AutoSpaceOptions with negative spacing to force overlap
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                // Negative horizontal spacing (in inches)
                DistanceInHorizontal = -0.2,
                // Negative vertical spacing (in inches)
                DistanceInVertical = -0.2
            };

            // Apply auto-spacing to all shapes on the page using the negative values
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
