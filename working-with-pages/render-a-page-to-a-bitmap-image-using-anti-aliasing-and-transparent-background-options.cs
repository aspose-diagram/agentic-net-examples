using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output image file path
        string outputPath = "output.png";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure image save options for PNG output
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);

                // Enable anti-aliasing (smoothing) for better visual quality
                options.SmoothingMode = Aspose.Diagram.Saving.SmoothingMode.AntiAlias;

                // Transparent background is not supported by ImageSaveOptions; this setting is omitted.

                // Export only the first page (index 0)
                options.PageIndex = 0;
                options.PageCount = 1;

                // Save the rendered page to the bitmap image
                diagram.Save(outputPath, options);

                Console.WriteLine($"Page rendered successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}