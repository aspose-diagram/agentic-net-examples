using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (change as needed)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page and export as a JPEG
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure JPEG export options
                    ImageSaveOptions jpegOptions = new ImageSaveOptions(SaveFileFormat.Jpeg)
                    {
                        // Export only the current page
                        PageIndex = i,
                        PageCount = 1,

                        // Optional: set JPEG quality (0-100). Adjust as needed.
                        JpegQuality = 90
                    };

                    // Note: If the library version supports progressive JPEGs,
                    // you can enable it via a property such as 'Progressive' here.
                    // Example (uncomment if available):
                    // jpegOptions.Progressive = true;

                    // Build output file name (e.g., Page_1.jpg)
                    string outputPath = $"Page_{i + 1}.jpg";

                    // Save the page as JPEG
                    diagram.Save(outputPath, jpegOptions);

                    Console.WriteLine($"Exported page {i + 1} to {outputPath}");
                }

                // No explicit disposal needed; Diagram does not implement IDisposable
                Console.WriteLine("All pages have been exported.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }