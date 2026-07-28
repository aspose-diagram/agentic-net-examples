using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class AutoSpaceBatchProcessor
{
    static void Main()
    {
        try
        {

            // Load the diagram file (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Define the desired spacing in inches
            const double horizontalSpacing = 0.5; // inches
            const double verticalSpacing = 0.5;   // inches

            // Iterate through all pages and apply auto‑spacing
            foreach (Page page in diagram.Pages)
            {
                // Configure auto‑space options
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions
                {
                    DistanceInHorizontal = horizontalSpacing,
                    DistanceInVertical = verticalSpacing
                };

                // Apply auto‑spacing to all shapes on the current page
                page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
            }

            // Save the modified diagram (adjust format and path as needed)
            DiagramSaveOptions saveOptions = new DiagramSaveOptions
            {
                AutoFitPageToDrawingContent = true // enlarge page to fit the new layout
            };
            diagram.Save("output.vsdx", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
