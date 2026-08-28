using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Get all shapes on the current page
                ShapeCollection shapes = page.Shapes;

                // Set up autospace options (custom distances in inches)
                AutoSpaceOptions options = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 0.5, // horizontal spacing
                    DistanceInVertical = 0.5    // vertical spacing
                };

                try
                {
                    // Attempt to auto‑space the shapes on the page
                    page.AutoSpaceShapes(shapes, options);
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during auto‑spacing
                    Console.Error.WriteLine($"Auto‑spacing failed on page {page.ID}: {ex.Message}");
                }
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
