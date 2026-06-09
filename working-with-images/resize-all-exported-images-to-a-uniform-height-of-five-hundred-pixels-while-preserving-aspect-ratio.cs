using System;
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

                // Load the diagram
                using (Diagram diagram = new Diagram(sourcePath))
                {
                    // Iterate through all pages in the diagram
                    for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                    {
                        // Retrieve the current page
                        Page page = diagram.Pages[pageIndex];

                        // Get page height in inches
                        double pageHeightInches = page.PageSheet.PageProps.PageHeight.Value;

                        // Define the desired output height in pixels
                        const int targetHeightPixels = 500;

                        // Use a standard resolution (dots per inch) for the image
                        const int resolutionDpi = 96;

                        // Calculate the current height in pixels
                        double currentHeightPixels = pageHeightInches * resolutionDpi;

                        // Compute scaling factor to achieve the target height while preserving aspect ratio
                        float scaleFactor = (float)(targetHeightPixels / currentHeightPixels);

                        // Configure image save options
                        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                        {
                            // Apply the scaling factor
                            Scale = scaleFactor,
                            // Set the resolution (optional, keeps DPI consistent)
                            Resolution = resolutionDpi,
                            // Export only the current page
                            PageIndex = pageIndex,
                            // Do not export hidden pages
                            ExportHiddenPage = false
                        };

                        // Build output file name
                        string outputPath = $"Page_{page.ID}_Export.png";

                        // Save the page as an image with the calculated scaling
                        diagram.Save(outputPath, saveOptions);
                    }
                }

                Console.WriteLine("All pages have been exported with a uniform height of 500 pixels.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }