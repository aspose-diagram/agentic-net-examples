using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, optional page index
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath> [pageIndex]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        // Guard: ensure the output directory exists (create if necessary)
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Determine which page to modify (default to first page)
        int pageIndex = 0;
        if (args.Length > 2 && !int.TryParse(args[2], out pageIndex))
        {
            Console.Error.WriteLine($"Invalid page index: {args[2]}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Validate the requested page index against the diagram's page count
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Page index out of range. Diagram contains {diagram.Pages.Count} pages.");
                return;
            }

            // Retrieve the target page
            Page page = diagram.Pages[pageIndex];

            // Set the print orientation of the page to Landscape
            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Dispose the diagram to release resources
            diagram.Dispose();

            Console.WriteLine($"Page {pageIndex} orientation set to Landscape and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}