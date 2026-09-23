using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - input Visio file path
        // 1 - page index (0‑based)
        // 2 - new width in inches (double)
        // 3 - new height in inches (double)
        // 4 - output Visio file path (optional)

        if (args.Length < 4)
        {
            Console.WriteLine("Usage: <inputFile> <pageIndex> <widthInInches> <heightInInches> [outputFile]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        if (!int.TryParse(args[1], out int pageIndex) || pageIndex < 0)
        {
            Console.WriteLine("Error: Invalid page index.");
            return;
        }

        if (!double.TryParse(args[2], out double newWidth) || newWidth <= 0)
        {
            Console.WriteLine("Error: Invalid width value.");
            return;
        }

        if (!double.TryParse(args[3], out double newHeight) || newHeight <= 0)
        {
            Console.WriteLine("Error: Invalid height value.");
            return;
        }

        string outputPath = args.Length >= 5 ? args[4] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_resized" + Path.GetExtension(inputPath));

        // Load the diagram
        using (Diagram diagram = new Diagram(inputPath))
        {
            if (pageIndex >= diagram.Pages.Count)
            {
                Console.WriteLine($"Error: Page index {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                return;
            }

            // Access the specified page
            Page page = diagram.Pages[pageIndex];

            // Resize the page
            page.PageSheet.PageProps.PageWidth.Value = newWidth;
            page.PageSheet.PageProps.PageHeight.Value = newHeight;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }

        Console.WriteLine($"Page {pageIndex} resized to {newWidth} x {newHeight} inches and saved to {outputPath}");
    }
}
