using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: input file must exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: output folder must exist (create if missing)
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Access the current page
                Page page = diagram.Pages[i];

                // Retrieve page width in inches
                double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;

                // Define desired output width in pixels
                const int targetWidthPixels = 800;

                // Set a base resolution (DPI) for the exported image
                const float resolutionDpi = 96f;

                // Calculate scale factor to achieve the target width while preserving aspect ratio
                // Scale = targetWidth / (pageWidthInches * resolution)
                float scale = (float)(targetWidthPixels / (pageWidthInches * resolutionDpi));

                // Configure image save options for BMP format
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Bmp)
                {
                    // Export only the current page
                    PageIndex = i,
                    PageCount = 1,
                    // Set resolution and scaling
                    Resolution = resolutionDpi,
                    Scale = scale
                };

                // Build output file path
                string outputPath = Path.Combine(outputFolder, $"page_{i + 1}.bmp");

                // Save the page as a BMP image with the calculated scaling
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Exported page {i + 1} to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}