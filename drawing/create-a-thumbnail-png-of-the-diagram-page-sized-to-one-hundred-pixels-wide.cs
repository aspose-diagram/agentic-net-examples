using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPngFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram does not contain any pages.");
                return;
            }

            // Access the first page (index 0).
            Page page = diagram.Pages[0];

            // Retrieve the page width in inches.
            double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;

            // Define the desired thumbnail width in pixels.
            const int targetWidthPixels = 100;

            // Use a default resolution (dots per inch). 96 DPI is common.
            const float resolutionDpi = 96f;

            // Calculate the scaling factor so that the rendered width equals the target width.
            // Scale = targetPixels / (pageWidthInches * DPI)
            float scale = (float)(targetWidthPixels / (pageWidthInches * resolutionDpi));

            // Configure image save options for PNG output.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Export only the first page.
                PageIndex = 0,
                PageCount = 1,

                // Set the resolution (optional, kept at default 96 DPI).
                Resolution = resolutionDpi,

                // Apply the calculated scaling factor.
                Scale = scale
            };

            // Save the thumbnail PNG.
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Thumbnail saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}