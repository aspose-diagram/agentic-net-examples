using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing Visio files (adjust as needed)
            string inputFolder = @"VisioFiles";
            // Output ZIP file path
            string outputZipPath = @"ShapeImages.zip";

            // Ensure the input folder exists
            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Input folder does not exist: {inputFolder}");
                return;
            }

            // Create or overwrite the ZIP archive
            using (FileStream zipStream = new FileStream(outputZipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                // Process each Visio file in the folder (common extensions)
                string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
                foreach (string visioPath in visioFiles)
                {
                    string ext = Path.GetExtension(visioPath).ToLowerInvariant();
                    if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx" && ext != ".vsx" && ext != ".vtx")
                    {
                        // Skip non‑Visio files
                        continue;
                    }

                    Console.WriteLine($"Processing: {Path.GetFileName(visioPath)}");

                    // Load the diagram
                    using (Diagram diagram = new Diagram(visioPath))
                    {
                        // Iterate all pages
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate all shapes on the page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Identify image (foreign) shapes
                                if (shape.Type == TypeValue.Foreign)
                                {
                                    // Prepare image export options (PNG format)
                                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);

                                    // Export shape image to a memory stream
                                    using (MemoryStream imgStream = new MemoryStream())
                                    {
                                        shape.ToImage(imgStream, imgOptions);
                                        imgStream.Position = 0; // Reset for reading

                                        // Build a unique entry name for the ZIP archive
                                        string entryName = $"{Path.GetFileNameWithoutExtension(visioPath)}_Page{page.ID}_Shape{shape.ID}.png";

                                        // Create ZIP entry and copy image data
                                        ZipArchiveEntry entry = archive.CreateEntry(entryName, CompressionLevel.Optimal);
                                        using (Stream entryStream = entry.Open())
                                        {
                                            imgStream.CopyTo(entryStream);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Image extraction completed. ZIP archive created at: {outputZipPath}");
        }
    }