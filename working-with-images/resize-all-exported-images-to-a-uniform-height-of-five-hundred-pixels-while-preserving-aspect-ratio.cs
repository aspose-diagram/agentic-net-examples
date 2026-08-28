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

                // Directory to store exported images
                string outputDir = "ExportedImages";
                Directory.CreateDirectory(outputDir);

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Desired uniform height in pixels
                const int targetHeightPixels = 500;

                // Assumed DPI for image rendering (default is 96)
                const double dpi = 96.0;

                // Iterate through each page in the diagram
                int pageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    // Get page height in inches
                    double pageHeightInches = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate scaling factor to achieve the target height
                    // (targetHeight = pageHeightInches * dpi * scale)
                    float scale = (float)(targetHeightPixels / (pageHeightInches * dpi));

                    // Configure image save options
                    ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        PageIndex = pageIndex,   // Export only the current page
                        PageCount = 1,
                        Scale = scale            // Preserve aspect ratio while fixing height
                    };

                    // Build output file name
                    string outputPath = Path.Combine(outputDir, $"Page_{pageIndex + 1}.png");

                    // Export the page as an image with the calculated scale
                    diagram.Save(outputPath, options);

                    pageIndex++;
                }

                // Clean up
                diagram.Dispose();

                Console.WriteLine("All pages exported with uniform height of 500 pixels.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }