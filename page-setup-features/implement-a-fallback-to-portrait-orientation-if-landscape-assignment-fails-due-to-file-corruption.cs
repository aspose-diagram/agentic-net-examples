using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram
        using (Diagram diagram = new Diagram(inputPath))
        {
            // Iterate through all pages and set orientation
            foreach (Page page in diagram.Pages)
            {
                try
                {
                    // Try to assign Landscape orientation
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }
                catch (Exception ex)
                {
                    // If assignment fails (e.g., due to file corruption), fallback to Portrait
                    Console.WriteLine($"Landscape assignment failed for page '{page.Name}': {ex.Message}");
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }
}
