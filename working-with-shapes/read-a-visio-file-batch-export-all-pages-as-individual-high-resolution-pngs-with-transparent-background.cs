using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output directory (same as input file directory)
        string outputDir = Path.GetDirectoryName(inputPath);
        if (string.IsNullOrEmpty(outputDir))
        {
            Console.Error.WriteLine("Unable to determine the output directory.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page and export it as a high‑resolution PNG
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Retrieve the page (explicitly typed)
                    Page page = diagram.Pages[i];

                    // Prepare the output file name for the current page
                    string outputFileName = $"{Path.GetFileNameWithoutExtension(inputPath)}_Page_{i + 1}.png";
                    string outputPath = Path.Combine(outputDir, outputFileName);

                    // Configure image save options
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    // Set a high resolution (e.g., 300 DPI)
                    saveOptions.Resolution = 300f;
                    // Enable anti‑aliasing for smoother rendering
                    saveOptions.SmoothingMode = Aspose.Diagram.Saving.SmoothingMode.AntiAlias;
                    // Export only the current page
                    saveOptions.PageIndex = i;
                    saveOptions.PageCount = 1;

                    // Save the page as PNG
                    diagram.Save(outputPath, saveOptions);

                    Console.WriteLine($"Exported page {i + 1} to {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}