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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (you can change the index as needed)
            Page page = diagram.Pages[0];

            // Configure auto-spacing distances (in inches)
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 0.5; // Horizontal spacing
            options.DistanceInVertical = 0.5;   // Vertical spacing

            // Apply auto-spacing to all shapes on the page
            page.AutoSpaceShapes(page.Shapes, options);

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
