using System.IO;
using System;
using Aspose.Diagram;

using Aspose.Diagram.AutoLayout; // Namespace for AutoSpaceOptions (if needed)

class AutoSpaceExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Create autospace options – set desired horizontal and vertical distances (in inches)
                AutoSpaceOptions options = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 0.5, // 0.5 inch horizontal spacing
                    DistanceInVertical = 0.5    // 0.5 inch vertical spacing
                };

                // Apply autospace to all shapes on the current page
                page.AutoSpaceShapes(page.Shapes, options);

                // Optional: center the drawing after spacing
                page.CenterDrawing();
            }

            // Save the modified diagram (replace with your desired output path and format)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
