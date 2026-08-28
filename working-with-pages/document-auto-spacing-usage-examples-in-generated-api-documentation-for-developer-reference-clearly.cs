using System;
using System.IO;
using Aspose.Diagram;

class AutoSpaceExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page of the diagram (you can iterate over diagram.Pages if needed)
            Page page = diagram.Pages[0];

            // Create AutoSpaceOptions and set custom spacing values (in inches)
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 0.5; // Horizontal spacing of 0.5 inch
            options.DistanceInVertical = 0.5;   // Vertical spacing of 0.5 inch

            // Apply auto‑spacing to all shapes on the selected page
            page.AutoSpaceShapes(page.Shapes, options);

            // Save the modified diagram (choose the desired format)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
