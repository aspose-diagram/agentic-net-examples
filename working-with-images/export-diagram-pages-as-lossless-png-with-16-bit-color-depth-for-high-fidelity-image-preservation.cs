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

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output folder exists
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
            // Load the Visio diagram using the mandatory using pattern
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Prepare PNG export options
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        // Export a single page at a time
                        PageIndex = i,
                        PageCount = 1,
                        // Use a high resolution to preserve detail (300 DPI)
                        Resolution = 300f
                        // Note: Aspose.Diagram does not expose a direct 16‑bit color depth setting.
                        // High‑resolution PNG provides lossless, high‑fidelity output.
                    };

                    // Construct output file name (e.g., Page_1.png)
                    string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.png");

                    // Save the current page as PNG
                    diagram.Save(outputPath, pngOptions);
                    Console.WriteLine($"Exported page {i + 1} to {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}