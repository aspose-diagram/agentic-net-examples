using System.IO;
using System;
using Aspose.Diagram;

using Aspose.Diagram.AutoLayout; // Namespace for AutoSpaceOptions

class AutoSpaceExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure auto‑spacing options (distances are in inches)
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5, // horizontal spacing
                DistanceInVertical = 0.5    // vertical spacing
            };

            // Apply auto‑spacing to every page in the document
            foreach (Page page in diagram.Pages)
            {
                // Auto‑space all shapes on the current page
                page.AutoSpaceShapes(page.Shapes, options);

                // Optional: re‑center the drawing after spacing
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
