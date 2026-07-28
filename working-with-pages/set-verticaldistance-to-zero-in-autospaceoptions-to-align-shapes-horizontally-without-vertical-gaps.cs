using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page (or any specific page you need)
            Page page = diagram.Pages[0];

            // Create AutoSpaceOptions and set vertical distance to zero
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInVertical = 0;               // No vertical gaps
            options.DistanceInHorizontal = 0.375;        // Default horizontal spacing (optional)

            // Auto‑space the shapes on the page using the specified options
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
