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

            // Get the first page (or specify the required page index)
            Page page = diagram.Pages[0];

            // Create AutoSpaceOptions and set vertical distance to zero
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInVertical = 0; // Align shapes horizontally without vertical gaps

            // Apply auto spacing to all shapes on the page using the configured options
            page.AutoSpaceShapes(page.Shapes, options);

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
