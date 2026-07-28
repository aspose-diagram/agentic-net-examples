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

                // Output folder for preview images
                string outputFolder = "PreviewImages";

                // Ensure the output directory exists
                Directory.CreateDirectory(outputFolder);

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        // Retrieve the current page
                        Page page = diagram.Pages[i];

                        // Get page dimensions (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Calculate center position for the watermark
                        double centerX = pageWidth / 2.0;
                        double centerY = pageHeight / 2.0;

                        // Add a semi‑transparent watermark that covers the whole page
                        // Font size is specified in inches (0.5 inches ≈ 36 points)
                        page.AddText(
                            centerX,               // PinX (center X)
                            centerY,               // PinY (center Y)
                            pageWidth,             // Width of the text box (full page width)
                            pageHeight,            // Height of the text box (full page height)
                            "CONFIDENTIAL",        // Watermark text
                            "Arial",               // Font name
                            "#CCCCCC",             // Font color (light gray)
                            0.5);                  // Font size in inches

                        // Prepare image save options for PNG export of the current page only
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                        {
                            PageIndex = i,   // Export this specific page
                            PageCount = 1    // Export a single page
                        };

                        // Define the output image file name
                        string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}.png");

                        // Save the page as an image with the watermark overlay
                        diagram.Save(outputPath, imgOptions);
                    }
                }

                Console.WriteLine("Preview images with watermarks have been generated successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }