using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
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
                // Create autospace options (set desired distances in inches)
                AutoSpaceOptions options = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 0.5, // horizontal spacing
                    DistanceInVertical = 0.5    // vertical spacing
                };

                // Apply autospace to all shapes on the current page
                page.AutoSpaceShapes(page.Shapes, options);
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
