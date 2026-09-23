using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the modified Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page, modify PrintProps, and report the values
                foreach (Page page in diagram.Pages)
                {
                    // Modify orientation to Landscape and ScaleX to 0.75 (75%)
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    page.PageSheet.PrintProps.ScaleX.Value = 0.75;

                    // Report current settings
                    Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}");
                    Console.WriteLine($"  Orientation: {page.PageSheet.PrintProps.PrintPageOrientation.Value}");
                    Console.WriteLine($"  ScaleX: {page.PageSheet.PrintProps.ScaleX.Value}");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
