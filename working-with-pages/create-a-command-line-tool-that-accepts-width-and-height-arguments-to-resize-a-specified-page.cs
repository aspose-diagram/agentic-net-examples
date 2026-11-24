using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // args[0] - input Visio file path
        // args[1] - page index (0‑based)
        // args[2] - new page width (in inches)
        // args[3] - new page height (in inches)
        // args[4] - output Visio file path
        if (args.Length != 5)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <pageIndex> <width> <height> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string pageIndexStr = args[1];
        string widthStr = args[2];
        string heightStr = args[3];
        string outputPath = args[4];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse numeric arguments
        if (!int.TryParse(pageIndexStr, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Invalid page index. It must be a non‑negative integer.");
            return;
        }

        if (!double.TryParse(widthStr, out double newWidth) || newWidth <= 0)
        {
            Console.Error.WriteLine("Invalid width. It must be a positive number.");
            return;
        }

        if (!double.TryParse(heightStr, out double newHeight) || newHeight <= 0)
        {
            Console.Error.WriteLine("Invalid height. It must be a positive number.");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Validate page index
            if (pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Page index out of range. Diagram contains {diagram.Pages.Count} pages.");
                return;
            }

            // Access the specified page
            Page page = diagram.Pages[pageIndex];

            // Resize the page using PageProps
            page.PageSheet.PageProps.PageWidth.Value = newWidth;
            page.PageSheet.PageProps.PageHeight.Value = newHeight;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Page {pageIndex} resized to {newWidth} x {newHeight} inches and saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}