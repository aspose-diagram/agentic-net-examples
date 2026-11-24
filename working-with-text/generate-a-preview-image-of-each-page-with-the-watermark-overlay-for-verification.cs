using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputFolder]");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "PreviewImages");

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        try
        {
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory '{outputFolder}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page explicitly typed as Aspose.Diagram.Page
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Retrieve page dimensions
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate watermark position (full page)
                double pinX = 0; // left edge
                double pinY = 0; // bottom edge
                double width = pageWidth;
                double height = pageHeight;

                // Add watermark text shape covering the whole page
                // Font color is a light gray hex string; font size is set to 48 points
                page.AddText(pinX, pinY, width, height, "WATERMARK", "Calibri", "#C0C0C0", 48);

                // Prepare image save options for PNG preview of the current page only
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    PageIndex = i,
                    PageCount = 1,
                    // Optional: set a reasonable resolution for preview
                    Resolution = 150f
                };

                // Build output file path
                string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.png");

                // Export the page as PNG
                diagram.Save(outputPath, pngOptions);

                Console.WriteLine($"Generated preview for page {i + 1}: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}