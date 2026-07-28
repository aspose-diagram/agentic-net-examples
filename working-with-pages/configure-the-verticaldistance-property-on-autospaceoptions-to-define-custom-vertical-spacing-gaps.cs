using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Create AutoSpaceOptions and set a custom vertical spacing (in inches)
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInVertical = 1.0; // Example: 1 inch vertical gap

            // Retrieve the first page (or any target page) and its shape collection
            Page page = diagram.Pages[0];
            ShapeCollection shapes = page.Shapes;

            // Apply auto‑spacing using the configured options
            page.AutoSpaceShapes(shapes, options);

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
