using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Create AutoSpaceOptions instance
            AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();

            // Define custom horizontal spacing gap (in inches)
            autoSpaceOptions.DistanceInHorizontal = 1.0; // 1 inch gap

            // Apply the auto‑spacing to all shapes on the first page
            Page firstPage = diagram.Pages[0];
            firstPage.AutoSpaceShapes(firstPage.Shapes, autoSpaceOptions);

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
