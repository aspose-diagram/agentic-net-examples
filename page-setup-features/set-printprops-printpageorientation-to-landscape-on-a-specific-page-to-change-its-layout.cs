using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, optional page index
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile> [pageIndex]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        int pageIndex = 0; // default to first page

        if (args.Length >= 3 && !int.TryParse(args[2], out pageIndex))
        {
            Console.Error.WriteLine("Invalid page index argument.");
            return;
        }

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the requested page index exists
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                return;
            }

            // Retrieve the specific page
            Page page = diagram.Pages[pageIndex];

            // Set the print orientation to Landscape
            // PrintProps.PrintPageOrientation is an enum wrapper; assign its Value property
            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Page {pageIndex} orientation set to Landscape and saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}