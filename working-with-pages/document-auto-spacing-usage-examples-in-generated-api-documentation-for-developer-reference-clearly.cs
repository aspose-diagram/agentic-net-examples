using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class AutoSpaceExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Create AutoSpaceOptions and configure spacing in inches
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 0.5; // Horizontal spacing of 0.5 inch
            options.DistanceInVertical = 0.5;   // Vertical spacing of 0.5 inch

            // Apply auto-spacing to all shapes on the page
            page.AutoSpaceShapes(page.Shapes, options);

            // Save the updated diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
