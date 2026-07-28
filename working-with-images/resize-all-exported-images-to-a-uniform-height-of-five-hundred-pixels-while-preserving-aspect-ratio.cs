using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: DiagramImageResizer <inputVisioFile> <outputFolder>");
                return;
            }

            string inputPath = args[0];
            string outputFolder = args[1];

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input file not found: {inputPath}");
                return;
            }

            if (!Directory.Exists(outputFolder))
            {
                Console.WriteLine($"Output folder does not exist. Creating: {outputFolder}");
                Directory.CreateDirectory(outputFolder);
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Desired image height in pixels
            const int targetHeightPixels = 500;

            // Iterate through each page and export it as a PNG with uniform height
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Page height in inches (Visio uses inches for page dimensions)
                double pageHeightInches = page.PageSheet.PageProps.PageHeight.Value;

                if (pageHeightInches <= 0)
                {
                    Console.WriteLine($"Warning: Page {i} has non‑positive height. Skipping.");
                    continue;
                }

                // Calculate the resolution needed to achieve the target pixel height
                // pixels = inches * resolution  =>  resolution = pixels / inches
                float requiredResolution = (float)(targetHeightPixels / pageHeightInches);

                // Configure image save options
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Export only the current page
                    PageIndex = i,
                    PageCount = 1,

                    // Set the calculated resolution to get the desired height
                    Resolution = requiredResolution,

                    // Preserve aspect ratio (default behavior)
                    // No need to set Scale; it remains 1.0
                };

                // Build output file name
                string outputFile = Path.Combine(outputFolder, $"Page_{i + 1}.png");

                // Save the page as an image
                diagram.Save(outputFile, saveOptions);

                Console.WriteLine($"Exported page {i + 1} to {outputFile} (Resolution: {requiredResolution:F2} DPI)");
            }

            // Clean up
            diagram.Dispose();
            Console.WriteLine("All pages processed.");
        }
    }