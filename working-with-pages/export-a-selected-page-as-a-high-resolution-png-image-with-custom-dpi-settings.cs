using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <inputVisioPath> <pageIndex> <dpi> <outputPngPath>
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <pageIndex> <dpi> <outputPngPath>");
            return;
        }

        string inputPath = args[0];
        string pageIndexArg = args[1];
        string dpiArg = args[2];
        string outputPath = args[3];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Parse page index.
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Error: Invalid page index. It must be a non‑negative integer.");
            return;
        }

        // Parse DPI (resolution).
        if (!float.TryParse(dpiArg, out float dpi) || dpi <= 0)
        {
            Console.Error.WriteLine("Error: Invalid DPI value. It must be a positive number.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate requested page index against the diagram's page count.
                if (pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Error: Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                    return;
                }

                // Configure image export options.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Export only the selected page.
                    PageIndex = pageIndex,
                    PageCount = 1,
                    // Set custom DPI (resolution).
                    Resolution = dpi,
                    // Do not export hidden pages. (Changed from BOOL.False to bool false)
                    ExportHiddenPage = false
                };

                // Perform the export.
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Page {pageIndex} exported successfully to '{outputPath}' with DPI {dpi}.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
