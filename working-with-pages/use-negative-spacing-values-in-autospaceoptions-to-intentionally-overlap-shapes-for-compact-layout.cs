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

            // Get the first page (or any specific page you want to work with)
            Page page = diagram.Pages[0];

            // Get all shapes on the page
            ShapeCollection shapes = page.Shapes;

            // Create AutoSpaceOptions with negative spacing to force overlap
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                // Negative horizontal spacing (in inches)
                DistanceInHorizontal = -0.2,
                // Negative vertical spacing (in inches)
                DistanceInVertical = -0.2
            };

            // Apply auto spacing with the custom options
            page.AutoSpaceShapes(shapes, options);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
