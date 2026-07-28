using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the target page (e.g., the first page)
            Page page = diagram.Pages[0];

            // Select the shapes to be auto‑spaced; here we use all shapes on the page
            ShapeCollection shapes = page.Shapes;

            // Configure auto‑spacing options
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 0.5; // horizontal gap in inches
            options.DistanceInVertical = 0.5;   // vertical gap in inches

            // Apply auto‑spacing to the selected shape collection on the page
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
