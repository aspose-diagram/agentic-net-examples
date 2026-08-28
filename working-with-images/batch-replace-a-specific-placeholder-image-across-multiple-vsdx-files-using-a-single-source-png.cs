using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments:
            // args[0] - Folder containing VSDX files
            // args[1] - Name of the placeholder image shape (exact shape Name property)
            // args[2] - Path to the source PNG that will replace the placeholder
            // args[3] - (Optional) Output folder; if omitted, original files are overwritten

            if (args.Length < 3)
            {
                Console.WriteLine("Usage: BatchImageReplace <folderPath> <placeholderShapeName> <sourcePngPath> [outputFolder]");
                return;
            }

            string folderPath = args[0];
            string placeholderName = args[1];
            string sourcePngPath = args[2];
            string outputFolder = args.Length >= 4 ? args[3] : null;

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Error: Folder not found - {folderPath}");
                return;
            }

            if (!File.Exists(sourcePngPath))
            {
                Console.WriteLine($"Error: PNG file not found - {sourcePngPath}");
                return;
            }

            if (outputFolder != null && !Directory.Exists(outputFolder))
            {
                try
                {
                    Directory.CreateDirectory(outputFolder);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating output folder: {ex.Message}");
                    return;
                }
            }

            // Load PNG bytes once for reuse
            byte[] pngBytes;
            try
            {
                pngBytes = File.ReadAllBytes(sourcePngPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading PNG file: {ex.Message}");
                return;
            }

            // Process each VSDX file in the folder
            string[] vsdxFiles = Directory.GetFiles(folderPath, "*.vsdx");
            foreach (string filePath in vsdxFiles)
            {
                try
                {
                    // Load diagram
                    Diagram diagram = new Diagram(filePath, LoadFileFormat.Vsdx);

                    bool modified = false;

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify foreign (image) shapes with the specified placeholder name
                            if (shape.Type == TypeValue.Foreign && string.Equals(shape.Name, placeholderName, StringComparison.OrdinalIgnoreCase))
                            {
                                // Replace the embedded image data
                                shape.ForeignData.Value = pngBytes;

                                // Optionally update the shape's name to reflect the new image file name
                                shape.Name = Path.GetFileName(sourcePngPath);

                                modified = true;
                            }
                        }
                    }

                    if (modified)
                    {
                        // Determine where to save the updated diagram
                        string outputPath = outputFolder != null
                            ? Path.Combine(outputFolder, Path.GetFileName(filePath))
                            : filePath; // overwrite original

                        // Save using the correct overload (filename + SaveFileFormat)
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                        Console.WriteLine($"Updated: {outputPath}");
                    }
                    else
                    {
                        Console.WriteLine($"No placeholder found in: {filePath}");
                    }

                    // Dispose diagram resources
                    diagram.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch replacement completed.");
        }
    }