using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (modify as needed)
                string inputPath = "input.vsdx";

                // Output directory for PNG files
                string outputDir = "ExportedPages";
                Directory.CreateDirectory(outputDir);

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page and export as a high‑resolution PNG with transparent background
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Build output file name (e.g., Page_1.png, Page_2.png, ...)
                    string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.png");

                    // Configure image save options
                    ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        // Set a high resolution (e.g., 300 DPI)
                        Resolution = 300f,
                        // Export only the current page
                        PageIndex = i,
                        PageCount = 1,
                        // Do not export hidden pages
                        ExportHiddenPage = false
                    };

                    // Save the specific page as PNG
                    diagram.Save(outputPath, options);
                }

                Console.WriteLine("Export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }