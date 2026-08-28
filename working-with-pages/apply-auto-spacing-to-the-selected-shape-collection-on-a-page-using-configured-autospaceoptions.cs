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

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from the input file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Configure auto‑spacing options
                AutoSpaceOptions options = new AutoSpaceOptions();
                options.DistanceInHorizontal = 2.0; // horizontal spacing in inches
                options.DistanceInVertical = 2.0;   // vertical spacing in inches

                // Apply auto‑spacing to all shapes on the page
                page.AutoSpaceShapes(page.Shapes, options);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Auto‑spacing applied and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
