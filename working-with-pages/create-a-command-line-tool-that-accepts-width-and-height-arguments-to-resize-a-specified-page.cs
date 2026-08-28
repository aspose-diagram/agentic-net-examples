using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // args[0] - input Visio file path
        // args[1] - output Visio file path
        // args[2] - new page width (in inches)
        // args[3] - new page height (in inches)
        // args[4] - (optional) page index (0‑based). Default is 0.

        if (args.Length < 4)
        {
            Console.WriteLine("Usage: <inputPath> <outputPath> <width> <height> [pageIndex]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        if (!double.TryParse(args[2], out double newWidth) || newWidth <= 0)
        {
            Console.WriteLine("Invalid width value.");
            return;
        }

        if (!double.TryParse(args[3], out double newHeight) || newHeight <= 0)
        {
            Console.WriteLine("Invalid height value.");
            return;
        }

        int pageIndex = 0;
        if (args.Length >= 5 && !int.TryParse(args[4], out pageIndex))
        {
            Console.WriteLine("Invalid page index value. Using default index 0.");
            pageIndex = 0;
        }

        try
        {
            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate page index
                if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                {
                    Console.WriteLine($"Page index {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                    return;
                }

                // Access the specified page
                Page page = diagram.Pages[pageIndex];

                // Set new dimensions (values are in inches)
                page.PageSheet.PageProps.PageWidth.Value = newWidth;
                page.PageSheet.PageProps.PageHeight.Value = newHeight;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Page {pageIndex} resized to {newWidth} x {newHeight} inches and saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
