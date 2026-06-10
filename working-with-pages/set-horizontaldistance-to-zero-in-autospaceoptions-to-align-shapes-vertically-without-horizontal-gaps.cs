using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Select the first page (or any specific page you want to modify)
            Aspose.Diagram.Page page = diagram.Pages[0];

            // Create AutoSpaceOptions and set horizontal distance to zero
            Aspose.Diagram.AutoSpaceOptions autoSpaceOptions = new Aspose.Diagram.AutoSpaceOptions();
            autoSpaceOptions.DistanceInHorizontal = 0.0;   // No horizontal gap
            autoSpaceOptions.DistanceInVertical = 0.375; // Keep default vertical spacing (optional)

            // Apply auto‑spacing to all shapes on the page
            page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

            // Save the modified diagram
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
