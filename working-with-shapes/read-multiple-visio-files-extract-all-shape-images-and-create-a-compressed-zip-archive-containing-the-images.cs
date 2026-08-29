using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq; // needed for Any()
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input folder containing Visio files (default to current directory)
        string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
        // Output ZIP file path (default to current directory)
        string outputZipPath = args.Length > 1 ? args[1] : Path.Combine(Directory.GetCurrentDirectory(), "VisioImages.zip");

        // Guard: ensure the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Collect all files in the folder
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        // Supported Visio extensions
        List<string> supportedExtensions = new List<string>
        {
            ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vstx",
            ".vsdm", ".vssm", ".vstm", ".vdw", ".vss", ".vst", ".html", ".mmd"
        };
        List<string> filesToProcess = new List<string>();

        // Filter files by supported extensions (case‑insensitive)
        foreach (var file in visioFiles)
        {
            if (supportedExtensions.Any(ext => string.Equals(ext, Path.GetExtension(file), StringComparison.OrdinalIgnoreCase)))
            {
                filesToProcess.Add(file);
            }
        }

        // Guard: ensure there is at least one Visio file to process
        if (filesToProcess.Count == 0)
        {
            Console.Error.WriteLine("No Visio files found in the specified folder.");
            return;
        }

        // Create the ZIP archive that will hold all extracted images
        using (FileStream zipToCreate = new FileStream(outputZipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
        {
            // Process each Visio file individually
            foreach (string visioPath in filesToProcess)
            {
                // Guard: ensure the Visio file actually exists before loading
                if (!File.Exists(visioPath))
                {
                    Console.Error.WriteLine($"File not found: {visioPath}");
                    continue;
                }

                try
                {
                    // Load the diagram from the file
                    Diagram diagram = new Diagram(visioPath);

                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Identify foreign (image) shapes
                            if (shape.Type == TypeValue.Foreign)
                            {
                                // Configure PNG export options
                                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);

                                // Build a unique entry name for the ZIP file
                                string entryName = $"{Path.GetFileNameWithoutExtension(visioPath)}_Page{page.ID}_Shape{shape.ID}.png";

                                // Export the shape to a temporary PNG file
                                string tempPngPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
                                shape.ToImage(tempPngPath, imgOptions);

                                // Add the PNG file to the ZIP archive
                                ZipArchiveEntry entry = archive.CreateEntry(entryName, CompressionLevel.Optimal);
                                using (Stream entryStream = entry.Open())
                                using (FileStream pngFile = new FileStream(tempPngPath, FileMode.Open, FileAccess.Read))
                                {
                                    pngFile.CopyTo(entryStream);
                                }

                                // Delete the temporary PNG file
                                File.Delete(tempPngPath);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Report any errors that occur while processing a file
                    Console.Error.WriteLine($"Error processing file '{visioPath}': {ex.Message}");
                }
            }
        }

        Console.WriteLine($"Image extraction completed. ZIP archive created at: {outputZipPath}");
    }
}