using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output folder for thumbnails
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioThumbnailGenerator <inputVisioPath> <outputFolder>");
                return;
            }

            string inputPath = args[0];
            string outputFolder = args[1];

            // Validate input file
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input file not found: {inputPath}");
                return;
            }

            // Ensure output directory exists
            Directory.CreateDirectory(outputFolder);

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page and generate a thumbnail
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Build thumbnail file name (e.g., Page_1.png)
                    string thumbnailPath = Path.Combine(outputFolder, $"Page_{i + 1}.png");

                    // Configure image save options for the specific page
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        PageIndex = i,      // Zero‑based page index
                        PageCount = 1,      // Export only this page
                        Resolution = 96f    // DPI (optional, adjust as needed)
                    };

                    // Save the page as an image thumbnail
                    diagram.Save(thumbnailPath, saveOptions);

                    Console.WriteLine($"Thumbnail saved: {thumbnailPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }