using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder containing VDX files.
            string inputFolder;
            if (args.Length > 0)
            {
                inputFolder = args[0];
            }
            else
            {
                Console.Write("Enter the full path to the folder containing VDX files: ");
                inputFolder = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Folder not found: {inputFolder}");
                return;
            }

            // Create an output folder for extracted images.
            string outputFolder = Path.Combine(inputFolder, "ExtractedImages");
            Directory.CreateDirectory(outputFolder);

            // Process each VDX file in the folder.
            string[] vdxFiles = Directory.GetFiles(inputFolder, "*.vdx", SearchOption.TopDirectoryOnly);
            foreach (string vdxPath in vdxFiles)
            {
                try
                {
                    // Load the Visio diagram.
                    Diagram diagram = new Diagram(vdxPath, LoadFileFormat.Vdx);

                    // Iterate through all pages and shapes.
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify foreign (image) shapes.
                            if (shape.Type == TypeValue.Foreign)
                            {
                                // Build a unique file name using the original VDX name and shape ID.
                                string baseName = Path.GetFileNameWithoutExtension(vdxPath);
                                string imageFileName = $"{baseName}_shape{shape.ID}.png";
                                string imagePath = Path.Combine(outputFolder, imageFileName);

                                // Export the shape to a PNG image.
                                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                                shape.ToImage(imagePath, imgOptions);

                                Console.WriteLine($"Extracted image: {imagePath}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{vdxPath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }