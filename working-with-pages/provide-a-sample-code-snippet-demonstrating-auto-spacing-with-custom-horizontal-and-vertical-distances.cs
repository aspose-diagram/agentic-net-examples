using System;
using System.IO;
using Aspose.Diagram;

class AutoSpaceExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve all shapes on the page (excluding background shapes)
            ShapeCollection shapes = page.Shapes;

            // Create AutoSpaceOptions with custom horizontal and vertical distances (in inches)
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 1.0; // 1 inch horizontal spacing
            options.DistanceInVertical = 0.5;   // 0.5 inch vertical spacing

            // Apply auto‑spacing to the selected shapes using the custom options
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
