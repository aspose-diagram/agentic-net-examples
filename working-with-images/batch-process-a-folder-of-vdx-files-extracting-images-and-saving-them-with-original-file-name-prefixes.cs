using System;
using System.IO;
using Aspose.Diagram;

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
                    Diagram diagram = new Diagram(vdxPath);

                    // Use the original file name (without extension) as a prefix.
                    string filePrefix = Path.GetFileNameWithoutExtension(vdxPath);

                    // Iterate through all pages and shapes to find embedded images.
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Image shapes are of type Foreign.
                            if (shape.Type == TypeValue.Foreign)
                            {
                                // The raw image bytes are stored in ForeignData.
                                byte[] imageData = shape.ForeignData?.Value;
                                if (imageData != null && imageData.Length > 0)
                                {
                                    // Build a unique file name: <originalFile>_Shape<ID>.png
                                    string imageFileName = $"{filePrefix}_Shape{shape.ID}.png";
                                    string imagePath = Path.Combine(outputFolder, imageFileName);

                                    // Write the image bytes to disk.
                                    File.WriteAllBytes(imagePath, imageData);
                                    Console.WriteLine($"Extracted image to: {imagePath}");
                                }
                            }
                        }
                    }

                    // Dispose the diagram to release resources.
                    diagram.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing '{vdxPath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }