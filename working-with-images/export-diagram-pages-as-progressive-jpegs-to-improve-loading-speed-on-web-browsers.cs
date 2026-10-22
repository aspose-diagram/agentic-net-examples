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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure JPEG export options for the current page.
                    ImageSaveOptions jpegOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                    jpegOptions.PageIndex = i;      // Export only this page.
                    jpegOptions.PageCount = 1;
                    jpegOptions.JpegQuality = 90;   // Reasonable quality for web use.

                    // Build the output file name (e.g., Page_1.jpg).
                    string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.jpg");

                    // Save the page as a JPEG image.
                    diagram.Save(outputPath, jpegOptions);
                }
            }

            Console.WriteLine("Export completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}