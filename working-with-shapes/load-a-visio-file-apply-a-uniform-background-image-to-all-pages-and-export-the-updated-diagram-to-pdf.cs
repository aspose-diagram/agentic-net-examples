using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Path to the background image that will be used uniformly
            string backgroundImagePath = "background.png";

            // Apply the background image to every page in the diagram
            // Here we add a shape named "Background" that can serve as a placeholder for the image.
            // The actual image assignment would be done via shape fill properties (not shown here).
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Add a shape that covers the page (using arbitrary size; adjust as needed)
                // Parameters: PinX, PinY, Width, Height, MasterName, PageIndex
                diagram.AddShape(0, 0, 10, 10, "Background", pageIndex);
            }

            // Export the updated diagram to PDF using the Save method with SaveFileFormat.Pdf
            diagram.Save("output.pdf", SaveFileFormat.Pdf);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
