using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output directory for JPEG images
                string outputDir = "ExportedImages";
                Directory.CreateDirectory(outputDir);

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Export each page as a JPEG with 20% increased brightness
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Jpeg);
                    options.PageIndex = i;               // Export current page
                    options.ImageBrightness = 1.2f;      // Increase brightness by 20%

                    string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.jpg");
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