using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments: input Visio file and output thumbnail path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramThumbnailGenerator <inputVisioFile> <outputThumbnailPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Ensure the input file exists
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input file not found: {inputPath}");
                return;
            }

            try
            {
                // Load the Visio diagram from the specified file
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configure image save options for a low‑resolution thumbnail
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        // Render only the first page (index 0)
                        PageIndex = 0,
                        PageCount = 1,

                        // Set a low resolution (e.g., 72 DPI)
                        Resolution = 72f,

                        // Scale down the image to reduce size further (optional)
                        Scale = 0.5f
                    };

                    // Save the rendered thumbnail image
                    diagram.Save(outputPath, saveOptions);

                    Console.WriteLine($"Thumbnail generated successfully: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while generating the thumbnail: {ex.Message}");
            }
        }
    }