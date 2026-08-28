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

            // Create AutoSpaceOptions and set a custom vertical spacing (in inches)
            AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
            autoSpaceOptions.DistanceInVertical = 1.0; // example: 1 inch gap

            // Apply the auto‑spacing to all shapes on the first page
            Page page = diagram.Pages[0];
            page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
