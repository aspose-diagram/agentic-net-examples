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
                Console.WriteLine("Usage: ExtractEmbeddedImages <input.vdx> <output_directory>");
                return;
            }

            string inputPath = args[0];
            string outputDir = args[1];

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Ensure the output directory exists
            Directory.CreateDirectory(outputDir);

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vdx);

            int imageCount = 0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify embedded images: TypeValue.Foreign indicates a foreign (image) shape
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                    {
                        byte[] imageBytes = shape.ForeignData.Value;

                        // Build a file name using shape ID to ensure uniqueness
                        string fileName = $"image_{shape.ID}.png";
                        string outputPath = Path.Combine(outputDir, fileName);

                        // Write the raw image data to disk
                        try
                        {
                            File.WriteAllBytes(outputPath, imageBytes);
                            Console.WriteLine($"Extracted image to: {outputPath}");
                            imageCount++;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to write image for shape ID {shape.ID}: {ex.Message}");
                        }
                    }
                }
            }

            Console.WriteLine($"Extraction complete. Total images extracted: {imageCount}");
        }
    }