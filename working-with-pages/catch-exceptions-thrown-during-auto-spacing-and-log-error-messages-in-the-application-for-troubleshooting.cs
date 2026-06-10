using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure autospace options
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5, // inches
                DistanceInVertical = 0.5    // inches
            };

            // Get the first page and its shapes collection
            Page page = diagram.Pages[0];
            ShapeCollection shapes = page.Shapes;

            try
            {
                // Attempt to auto‑space the shapes on the page
                page.AutoSpaceShapes(shapes, options);
            }
            catch (Exception ex)
            {
                // Log any errors that occur during auto‑spacing
                Console.Error.WriteLine($"AutoSpaceShapes error: {ex.Message}");
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
