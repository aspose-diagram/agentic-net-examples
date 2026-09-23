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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (you can iterate over diagram.Pages if needed)
            Page page = diagram.Pages[0];

            // Configure auto‑spacing options (distances are in inches)
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 0.5; // Horizontal gap between shapes
            options.DistanceInVertical = 0.5;   // Vertical gap between shapes

            // Apply auto‑spacing to all shapes on the page
            page.AutoSpaceShapes(page.Shapes, options);

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Auto‑spacing applied. Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
