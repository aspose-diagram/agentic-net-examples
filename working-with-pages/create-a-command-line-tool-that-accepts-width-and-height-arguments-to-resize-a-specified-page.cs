using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <inputFile> <pageIndex> <newWidth> <newHeight> <outputFile>
        if (args.Length != 5)
        {
            Console.Error.WriteLine("Usage: Program <inputFile> <pageIndex> <newWidth> <newHeight> <outputFile>");
            return;
        }

        string inputPath = args[0];
        string pageIndexArg = args[1];
        string widthArg = args[2];
        string heightArg = args[3];
        string outputPath = args[4];

        // Guard: input file must exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Parse page index
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Error: Invalid page index. It must be a non‑negative integer.");
            return;
        }

        // Parse new width
        if (!double.TryParse(widthArg, out double newWidth) || newWidth <= 0)
        {
            Console.Error.WriteLine("Error: Invalid width. It must be a positive number.");
            return;
        }

        // Parse new height
        if (!double.TryParse(heightArg, out double newHeight) || newHeight <= 0)
        {
            Console.Error.WriteLine("Error: Invalid height. It must be a positive number.");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Validate page index range
            if (pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Error: Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                return;
            }

            // Access the specified page
            Page page = diagram.Pages[pageIndex];

            // Resize the page using PageProps
            page.PageSheet.PageProps.PageWidth.Value = newWidth;
            page.PageSheet.PageProps.PageHeight.Value = newHeight;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Page {pageIndex} resized to {newWidth} x {newHeight} inches and saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}