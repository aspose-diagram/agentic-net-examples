using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            var diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Define the auto‑spacing options to be applied to every page
            var autoSpaceOptions = new Aspose.Diagram.AutoSpaceOptions
            {
                // Set desired spacing in inches (adjust as needed)
                DistanceInHorizontal = 0.5, // horizontal gap between shapes
                DistanceInVertical   = 0.5  // vertical gap between shapes
            };

            // Apply the auto‑spacing to each page in the diagram
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // AutoSpaceShapes works on a collection of shapes; here we use all shapes on the page
                page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
            }

            // Save the modified diagram (replace with your desired output path and format)
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
