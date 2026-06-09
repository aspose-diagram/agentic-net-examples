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

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output directory for resized BMP images
                string outputDir = "ExportedBmp";
                Directory.CreateDirectory(outputDir);

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Desired width in pixels
                    const int targetWidthPixels = 800;

                    // Iterate through each page in the diagram
                    for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                    {
                        Page page = diagram.Pages[pageIndex];

                        // Get the page width in inches
                        double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;

                        // Use default resolution (dots per inch). You can change this if needed.
                        int dpi = 96;

                        // Calculate the scaling factor to achieve the target pixel width
                        // scale = targetPixels / (pageWidthInches * dpi)
                        float scale = (float)(targetWidthPixels / (pageWidthInches * dpi));

                        // Configure image save options for BMP format
                        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Bmp)
                        {
                            Resolution = dpi,
                            Scale = scale,
                            PageIndex = pageIndex // Export only the current page
                        };

                        // Build output file name (e.g., Page_1.bmp)
                        string outputPath = Path.Combine(outputDir, $"Page_{pageIndex + 1}.bmp");

                        // Save the page as a BMP image with the calculated scaling
                        diagram.Save(outputPath, saveOptions);
                    }
                }

                Console.WriteLine("BMP export and resizing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }