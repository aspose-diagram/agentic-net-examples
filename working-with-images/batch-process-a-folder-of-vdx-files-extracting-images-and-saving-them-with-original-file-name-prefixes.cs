using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder to process. If not provided, prompt the user.
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the full path to the folder containing VDX files: ");
            folderPath = Console.ReadLine()?.Trim() ?? string.Empty;
        }

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Create an output subfolder for extracted images.
        string outputFolder = Path.Combine(folderPath, "ExtractedImages");
        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory '{outputFolder}': {ex.Message}");
            return;
        }

        // Get all VDX files in the folder.
        string[] vdxFiles = Directory.GetFiles(folderPath, "*.vdx");
        if (vdxFiles.Length == 0)
        {
            Console.WriteLine("No VDX files found in the specified folder.");
            return;
        }

        // Process each VDX file.
        foreach (string vdxPath in vdxFiles)
        {
            // Guard: ensure the file still exists.
            if (!File.Exists(vdxPath))
            {
                Console.Error.WriteLine($"File not found (skipping): {vdxPath}");
                continue;
            }

            Console.WriteLine($"Processing file: {Path.GetFileName(vdxPath)}");

            try
            {
                // Load the diagram using the appropriate load format.
                Diagram diagram = new Diagram(vdxPath, LoadFileFormat.Vdx);

                // Use the file name (without extension) as the prefix for extracted images.
                string filePrefix = Path.GetFileNameWithoutExtension(vdxPath);
                int imageCounter = 1;

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify foreign (image) shapes.
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                        {
                            // Build the output image file name.
                            string imageFileName = $"{filePrefix}_image{imageCounter}.png";
                            string imagePath = Path.Combine(outputFolder, imageFileName);

                            // Write the raw image bytes to disk.
                            try
                            {
                                File.WriteAllBytes(imagePath, shape.ForeignData.Value);
                                Console.WriteLine($"  Extracted image: {imageFileName}");
                            }
                            catch (Exception writeEx)
                            {
                                Console.Error.WriteLine($"  Failed to write image '{imageFileName}': {writeEx.Message}");
                            }

                            imageCounter++;
                        }
                    }
                }

                // If no images were found, inform the user.
                if (imageCounter == 1)
                {
                    Console.WriteLine("  No embedded images found in this file.");
                }
            }
            catch (Exception ex)
            {
                // Capture any Aspose.Diagram related errors.
                Console.Error.WriteLine($"Error processing '{vdxPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}