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

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Directory where JPEG images will be saved
                string outputDir = "ExportedImages";

                // Ensure the output directory exists
                if (!Directory.Exists(outputDir))
                    Directory.CreateDirectory(outputDir);

                // Load the diagram from file
                Diagram diagram = new Diagram(sourcePath);

                // Iterate through each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Prepare JPEG save options with 20% brightness increase
                    ImageSaveOptions jpegOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                    jpegOptions.ImageBrightness = 0.2f; // Increase brightness by 20%
                    jpegOptions.PageIndex = i;          // Export current page
                    jpegOptions.PageCount = 1;          // Export only one page

                    // Build output file name (e.g., Page_1.jpg)
                    string outputPath = Path.Combine(outputDir, $"Page_{i + 1}.jpg");

                    // Save the page as JPEG with the specified options
                    diagram.Save(outputPath, jpegOptions);
                }

                Console.WriteLine("Export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }