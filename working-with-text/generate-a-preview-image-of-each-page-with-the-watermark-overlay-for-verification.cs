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

                // Output directory for preview images
                string outputDir = "output";
                Directory.CreateDirectory(outputDir);

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    int pageIndex = 0;

                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page dimensions (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Calculate center position for the watermark
                        double centerX = pageWidth / 2.0;
                        double centerY = pageHeight / 2.0;

                        // Add watermark text covering the full page
                        // Font size is specified in inches (0.25 inches ≈ 18 points)
                        page.AddText(
                            centerX,               // PinX (center X)
                            centerY,               // PinY (center Y)
                            pageWidth,             // Width of the text box (full page width)
                            pageHeight,            // Height of the text box (full page height)
                            "CONFIDENTIAL",        // Watermark text
                            "Calibri",             // Font name
                            "#a5a5a5",             // Font color (hex)
                            0.25);                 // Font size in inches

                        // Configure image save options for PNG export of the current page only
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                        {
                            PageIndex = pageIndex, // Zero‑based page index
                            PageCount = 1          // Export a single page
                        };

                        // Build output file name
                        string outputPath = Path.Combine(outputDir, $"Page_{pageIndex}_preview.png");

                        // Save the page as an image with the watermark applied
                        diagram.Save(outputPath, imgOptions);

                        pageIndex++;
                    }
                }

                Console.WriteLine("Preview images with watermarks have been generated.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }