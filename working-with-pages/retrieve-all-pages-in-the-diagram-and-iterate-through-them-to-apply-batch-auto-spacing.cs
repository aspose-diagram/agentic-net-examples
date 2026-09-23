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

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Configure auto‑spacing options
            AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
            autoSpaceOptions.DistanceInHorizontal = 2;
            autoSpaceOptions.DistanceInVertical = 2;

            // Apply auto‑spacing to each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
            }

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
