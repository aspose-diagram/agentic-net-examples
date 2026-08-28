using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that a second page exists (pages are zero‑based)
                if (diagram.Pages.Count < 2)
                {
                    Console.WriteLine("The diagram does not contain a second page.");
                    return;
                }

                // Retrieve the second page (index 1)
                Page secondPage = diagram.Pages[1];

                // Read current page dimensions
                double currentWidth = secondPage.PageSheet.PageProps.PageWidth.Value;
                double currentHeight = secondPage.PageSheet.PageProps.PageHeight.Value;
                Console.WriteLine($"Original size – Width: {currentWidth} in, Height: {currentHeight} in");

                // Set the page height to 11 inches
                secondPage.PageSheet.PageProps.PageHeight.Value = 11.0;
                Console.WriteLine("Page height set to 11 inches.");

                // Export the modified diagram to VDX format
                string outputPath = "output.vdx";
                diagram.Save(outputPath, SaveFileFormat.Vdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
