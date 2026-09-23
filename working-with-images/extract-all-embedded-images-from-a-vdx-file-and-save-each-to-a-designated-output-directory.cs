using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments: args[0] = input VDX file, args[1] = output directory
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramImageExtractor <input.vdx> <output_directory>");
                return;
            }

            string inputPath = args[0];
            string outputDir = args[1];

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input file not found: {inputPath}");
                return;
            }

            // Ensure the output directory exists
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            int imageCounter = 0;

            // Iterate through all pages and shapes to find embedded images
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify foreign (image) shapes
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // The raw image bytes are stored in ForeignData.Value
                        byte[] imageData = shape.ForeignData?.Value;
                        if (imageData == null || imageData.Length == 0)
                        {
                            continue; // No data to write
                        }

                        // Determine a file name for the extracted image
                        string fileName = $"image_{++imageCounter}.png"; // Default to PNG
                        string outputPath = Path.Combine(outputDir, fileName);

                        // Write the image bytes to disk
                        try
                        {
                            File.WriteAllBytes(outputPath, imageData);
                            Console.WriteLine($"Extracted image saved to: {outputPath}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to save image {fileName}: {ex.Message}");
                        }
                    }
                }
            }

            Console.WriteLine($"Extraction complete. {imageCounter} image(s) saved to '{outputDir}'.");
        }
    }