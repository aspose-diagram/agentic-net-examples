using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Set up autospace options (distance in inches)
            AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5, // horizontal spacing
                DistanceInVertical = 0.5    // vertical spacing
            };

            // Iterate through all pages and apply auto‑spacing to each page's shapes
            foreach (Page page in diagram.Pages)
            {
                // Auto space all shapes on the current page using the defined options
                page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
