using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output image file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.png";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure image save options for PNG output
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);

                // Enable anti-aliasing for smoother rendering
                options.SmoothingMode = Aspose.Diagram.Saving.SmoothingMode.AntiAlias;

                // Transparent background is not supported by Aspose.Diagram ImageSaveOptions.
                // If a transparent background is required, additional post‑processing would be needed.

                // Render only the first page (index 0)
                options.PageIndex = 0;
                options.PageCount = 1;

                // Save the rendered page as a bitmap image
                diagram.Save(outputPath, options);
            }

            Console.WriteLine($"Page rendered successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}