using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string filePath = "input.vsdx";

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(filePath))
            {
                // Iterate through all pages by index
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Retrieve the page object
                    Aspose.Diagram.Page page = diagram.Pages[i];

                    // Get the page orientation (Landscape, Portrait, etc.)
                    PrintPageOrientationValue orientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;

                    // Get the horizontal scaling factor (ScaleX)
                    double scaleX = page.PageSheet.PrintProps.ScaleX.Value;

                    // Output the information to the console
                    Console.WriteLine($"Page Index: {i}, Orientation: {orientation}, ScaleX: {scaleX}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
