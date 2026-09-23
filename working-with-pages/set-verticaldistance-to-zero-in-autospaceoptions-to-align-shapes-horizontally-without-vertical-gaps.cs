using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Configure AutoSpaceOptions to remove vertical gaps
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInVertical = 0; // Set vertical distance to zero
            // Optionally, you can also set horizontal distance if needed
            // options.DistanceInHorizontal = 0.5;

            // Apply auto-spacing to the shapes on the page
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
