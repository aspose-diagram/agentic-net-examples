using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to process. Use the first argument if provided, otherwise the current directory.
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder does not exist: {folderPath}");
                return;
            }

            // Get all VDX files in the folder.
            string[] vdxFiles = Directory.GetFiles(folderPath, "*.vdx", SearchOption.TopDirectoryOnly);
            if (vdxFiles.Length == 0)
            {
                Console.WriteLine("No VDX files found in the specified folder.");
                return;
            }

            foreach (string vdxFile in vdxFiles)
            {
                try
                {
                    // Load the Visio diagram.
                    Diagram diagram = new Diagram(vdxFile);

                    int imageIndex = 1;
                    string baseFileName = Path.GetFileNameWithoutExtension(vdxFile);

                    // Iterate through all pages and shapes to find image (foreign) shapes.
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Image shapes are stored as foreign shapes.
                            if (shape.Type == TypeValue.Foreign)
                            {
                                // The raw image bytes are stored in the ForeignData cell.
                                byte[] imageData = shape.ForeignData?.Value;
                                if (imageData != null && imageData.Length > 0)
                                {
                                    // Save the image using the original file name as a prefix.
                                    string outputFileName = $"{baseFileName}_image{imageIndex}.png";
                                    string outputPath = Path.Combine(folderPath, outputFileName);

                                    File.WriteAllBytes(outputPath, imageData);
                                    Console.WriteLine($"Extracted image to: {outputPath}");
                                    imageIndex++;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{vdxFile}': {ex.Message}");
                }
            }

            Console.WriteLine("Image extraction completed.");
        }
    }