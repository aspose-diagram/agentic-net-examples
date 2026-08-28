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

            // Create AutoSpaceOptions and set custom horizontal spacing (in inches)
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 1.0; // 1 inch horizontal gap
            options.DistanceInVertical = 0.5;   // optional vertical gap

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Apply auto-spacing to all shapes on the page using the custom options
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
