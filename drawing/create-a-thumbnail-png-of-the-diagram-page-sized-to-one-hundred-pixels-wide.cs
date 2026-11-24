using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PNG thumbnail path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPngPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve the page width (in inches)
                double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;

                // Desired thumbnail width in pixels
                const int targetPixelWidth = 100;

                // Default DPI for image export (Aspose uses 96 DPI if not set)
                const float defaultDpi = 96f;

                // Calculate scaling factor to achieve the target pixel width
                // Scale = targetPixels / (pageWidthInches * DPI)
                float scale = (float)(targetPixelWidth / (pageWidthInches * defaultDpi));

                // Configure image save options for PNG output
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Export only the first page
                    PageIndex = 0,
                    PageCount = 1,
                    // Apply the calculated scaling factor
                    Scale = scale
                };

                // Save the thumbnail PNG
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Thumbnail saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}