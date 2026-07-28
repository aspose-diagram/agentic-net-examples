using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output folder path.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <program> <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Load the Visio diagram.
        using (Diagram diagram = new Diagram(inputPath))
        {
            // Iterate through all pages in the diagram.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                var page = diagram.Pages[i];

                // Get page dimensions in inches.
                double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeightInches = page.PageSheet.PageProps.PageHeight.Value;

                // Desired output width in pixels.
                const int targetWidthPx = 800;

                // Calculate target height to maintain aspect ratio.
                int targetHeightPx = (int)Math.Round(targetWidthPx * pageHeightInches / pageWidthInches);

                // Configure image save options for BMP with the calculated size.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Bmp);
                saveOptions.PageSize = new PageSize(targetWidthPx, targetHeightPx);

                // Build output file name.
                string outputPath = System.IO.Path.Combine(outputFolder, $"Page_{i + 1}.bmp");

                // Save the page as a BMP image with the specified dimensions.
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Exported page {i + 1} to {outputPath} (Width: {targetWidthPx}px, Height: {targetHeightPx}px)");
            }
        }
    }
}
