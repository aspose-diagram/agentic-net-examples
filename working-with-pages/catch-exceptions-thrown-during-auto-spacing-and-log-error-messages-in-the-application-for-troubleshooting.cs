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

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Set up auto‑spacing options
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5, // inches
                DistanceInVertical = 0.5    // inches
            };

            try
            {
                // Perform auto‑spacing on all shapes of the page
                page.AutoSpaceShapes(page.Shapes, options);
                Console.WriteLine("Auto‑spacing completed successfully.");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during auto‑spacing
                Console.WriteLine($"Error during auto‑spacing: {ex.Message}");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
