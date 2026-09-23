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

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and attempt to set Landscape orientation
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        // Attempt to assign Landscape orientation
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }
                    catch (Exception ex)
                    {
                        // If setting Landscape fails (e.g., due to file corruption), fallback to Portrait
                        Console.WriteLine($"Failed to set Landscape orientation on page {page.ID}: {ex.Message}");
                        Console.WriteLine("Falling back to Portrait orientation.");

                        // Apply Portrait orientation as a safe default
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
