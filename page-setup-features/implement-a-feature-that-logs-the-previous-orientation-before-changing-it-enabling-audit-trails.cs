using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (or any specific page by index/name)
                Page page = diagram.Pages[0];

                // Retrieve and log the current (previous) orientation
                PrintPageOrientationValue previousOrientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;
                Console.WriteLine($"Previous orientation: {previousOrientation}");

                // Change the orientation (example: set to Landscape)
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                Console.WriteLine($"New orientation applied: {page.PageSheet.PrintProps.PrintPageOrientation.Value}");

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
