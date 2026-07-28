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

            // Load the Visio diagram (using the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and apply auto‑spacing
            foreach (Page page in diagram.Pages)
            {
                // Set up auto‑spacing options (distance in inches)
                AutoSpaceOptions options = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 0.5,
                    DistanceInVertical = 0.5
                };

                // Apply auto‑spacing to all shapes on the current page
                page.AutoSpaceShapes(page.Shapes, options);
            }

            // Save the modified diagram (using the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
