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

            // Get the first page (or any specific page you need)
            Page page = diagram.Pages[0];

            // Configure autospace options (distance in inches)
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5, // horizontal spacing
                DistanceInVertical = 0.5    // vertical spacing
            };

            // Auto‑space all shapes on the page
            page.AutoSpaceShapes(page.Shapes, options);

            // After auto‑spacing, refresh each shape and log its new position
            foreach (Shape shape in page.Shapes)
            {
                shape.RefreshData(); // ensure position data is current

                double x = shape.XForm.PinX.Value; // X coordinate (inches)
                double y = shape.XForm.PinY.Value; // Y coordinate (inches)

                Console.WriteLine($"Shape ID {shape.ID} new position: X = {x:F2} in, Y = {y:F2} in");
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
