using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioImageExtractor <inputVisioPath> <outputZipPath>");
                return;
            }

            string inputVisioPath = args[0];
            string outputZipPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputVisioPath);

            // Create a temporary folder to store extracted images
            string tempFolder = Path.Combine(Path.GetTempPath(), "VisioImages_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);

            try
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify image (foreign) shapes
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Build a unique file name for each shape image
                            string imageFileName = $"shape_{shape.ID}.png";
                            string imagePath = Path.Combine(tempFolder, imageFileName);

                            // Configure image export options: PNG format with grayscale conversion
                            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                            {
                                ImageColorMode = ImageColorMode.Grayscale
                            };

                            // Export the shape to an image file
                            shape.ToImage(imagePath, imgOptions);
                        }
                    }
                }

                // Create the ZIP archive containing all exported images
                using (ZipArchive zip = ZipFile.Open(outputZipPath, ZipArchiveMode.Create))
                {
                    foreach (string filePath in Directory.GetFiles(tempFolder))
                    {
                        zip.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                    }
                }

                Console.WriteLine($"Extraction complete. Images archived to: {outputZipPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
            finally
            {
                // Clean up temporary files
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
        }
    }