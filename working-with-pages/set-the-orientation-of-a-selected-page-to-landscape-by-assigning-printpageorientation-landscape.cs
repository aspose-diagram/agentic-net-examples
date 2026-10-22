using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <pageIndex>");
            return;
        }

        string inputPath = args[0];
        string pageIndexArg = args[1];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse page index
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine($"Invalid page index: {pageIndexArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Validate page index range
            if (pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                return;
            }

            // Retrieve the selected page
            Page page = diagram.Pages[pageIndex];

            // Set the page orientation to Landscape
            // PrintProps resides under PageSheet.PrintProps
            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

            // Prepare output file path (adds "_landscape" suffix)
            string directory = Path.GetDirectoryName(inputPath) ?? "";
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_landscape.vsdx");

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Page {pageIndex} orientation set to Landscape. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}