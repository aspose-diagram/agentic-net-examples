using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting arguments: inputFilePath outputFilePath newPageWidth newPageHeight
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputFilePath> <outputFilePath> <newPageWidth> <newPageHeight>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string widthArg = args[2];
        string heightArg = args[3];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse new page dimensions
        if (!double.TryParse(widthArg, out double newWidth) || !double.TryParse(heightArg, out double newHeight))
        {
            Console.Error.WriteLine("Invalid page width or height. Please provide numeric values.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Change size of the first page (index 0)
                if (diagram.Pages.Count > 0)
                {
                    Page page = diagram.Pages[0];

                    // Set new width and height via PageProps
                    page.PageSheet.PageProps.PageWidth.Value = newWidth;
                    page.PageSheet.PageProps.PageHeight.Value = newHeight;

                    // TODO: Refresh page thumbnails after changing page size.
                    // The appropriate API method to refresh thumbnails is not identified in the current documentation.
                    // Insert the correct call here once the method is known, e.g., page.RefreshThumbnails() or diagram.RefreshThumbnails().
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}