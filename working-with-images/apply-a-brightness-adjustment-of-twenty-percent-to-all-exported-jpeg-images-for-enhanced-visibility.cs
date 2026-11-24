using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure output folder exists or create it
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
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure image save options for JPEG with 20% brightness increase
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                    saveOptions.ImageBrightness = 0.2f; // Increase brightness by 20%
                    saveOptions.JpegQuality = 90;       // Reasonable JPEG quality
                    saveOptions.PageIndex = i;          // Export current page
                    saveOptions.PageCount = 1;          // Export only this page

                    // Build output file path
                    string outputPath = Path.Combine(outputFolder, $"page_{i + 1}.jpg");

                    // Save the page as a JPEG image with the specified options
                    diagram.Save(outputPath, saveOptions);
                }
            }

            Console.WriteLine("JPEG export with brightness adjustment completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}