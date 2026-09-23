using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Configure auto‑spacing options
                    AutoSpaceOptions options = new AutoSpaceOptions
                    {
                        DistanceInHorizontal = 0.5, // horizontal spacing in inches
                        DistanceInVertical = 0.5    // vertical spacing in inches
                    };

                    // Apply auto‑spacing to all shapes on the current page
                    page.AutoSpaceShapes(page.Shapes, options);
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
