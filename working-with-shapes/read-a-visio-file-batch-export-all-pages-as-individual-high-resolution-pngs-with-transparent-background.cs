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
                Console.WriteLine("Usage: VisioBatchExport <inputVisioFile> <outputFolder>");
                return;
            }

            string inputPath = args[0];
            string outputFolder = args[1];

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input file not found: {inputPath}");
                return;
            }

            // Ensure output directory exists
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure high‑resolution PNG export options
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.Resolution = 300f;          // 300 DPI for high quality
            saveOptions.PageCount = 1;              // Export one page at a time

            // Iterate through each page and export as a separate PNG file
            foreach (Page page in diagram.Pages)
            {
                // Set the page index for the current page (zero‑based)
                saveOptions.PageIndex = (int)page.ID - 1;

                // Build output file name using page name (fallback to page ID)
                string safePageName = string.IsNullOrWhiteSpace(page.Name) ? $"Page_{page.ID}" : page.Name;
                string outputPath = Path.Combine(outputFolder, $"{safePageName}.png");

                try
                {
                    diagram.Save(outputPath, saveOptions);
                    Console.WriteLine($"Exported page '{safePageName}' to '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to export page '{safePageName}': {ex.Message}");
                }
            }
        }
    }