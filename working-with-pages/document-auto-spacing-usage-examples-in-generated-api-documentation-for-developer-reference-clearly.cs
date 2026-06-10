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

            // Create AutoSpaceOptions and configure spacing (in inches)
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 0.5; // Horizontal spacing between shapes
            options.DistanceInVertical = 0.5;   // Vertical spacing between shapes

            // Apply auto‑spacing to each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Auto space all shapes on the current page using the defined options
                page.AutoSpaceShapes(page.Shapes, options);
            }

            // Save the updated diagram (choose desired format)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
