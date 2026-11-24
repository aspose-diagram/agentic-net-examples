using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputPngFile]");
            return;
        }

        string inputPath = args[0];
        // Determine output path; default to same name with .png extension.
        string outputPath = args.Length > 1 ? args[1] : Path.ChangeExtension(inputPath, ".png");

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure image export options for high‑resolution PNG.
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                options.PageIndex = 0;          // Export only the first page.
                options.PageCount = 1;
                options.Resolution = 300f;      // Set DPI for high quality.
                options.SmoothingMode = Aspose.Diagram.Saving.SmoothingMode.AntiAlias; // Enable anti‑aliasing.

                // Save the first page as PNG.
                diagram.Save(outputPath, options);
                Console.WriteLine($"First page exported successfully to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}