using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input folder path and output zip file path
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: VisioImageExtractor <inputFolder> <outputZipPath>");
                return;
            }

            string inputFolder = args[0];
            string outputZipPath = args[1];

            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Input folder does not exist: {inputFolder}");
                return;
            }

            // Supported Visio extensions
            string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vstx", ".vsdm", ".vssm", ".vstm" };

            // Collect all Visio files in the folder
            List<string> visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly)
                                               .Where(f => extensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                                               .ToList();

            if (visioFiles.Count == 0)
            {
                Console.WriteLine("No Visio files found in the specified folder.");
                return;
            }

            // Create the ZIP archive
            using (FileStream zipStream = new FileStream(outputZipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                foreach (string visioPath in visioFiles)
                {
                    try
                    {
                        // Load the Visio diagram
                        Diagram diagram = new Diagram(visioPath);

                        // Iterate through all pages
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through all shapes on the page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Identify foreign (image) shapes
                                if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                                {
                                    byte[] imageBytes = shape.ForeignData.Value;

                                    // Build a unique file name for the image
                                    string baseName = Path.GetFileNameWithoutExtension(visioPath);
                                    string entryName = $"{baseName}_Page{page.ID}_Shape{shape.ID}.png";

                                    // Add the image to the ZIP archive
                                    ZipArchiveEntry entry = archive.CreateEntry(entryName);
                                    using (Stream entryStream = entry.Open())
                                    {
                                        entryStream.Write(imageBytes, 0, imageBytes.Length);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file '{visioPath}': {ex.Message}");
                    }
                }
            }

            Console.WriteLine($"Image extraction complete. ZIP archive created at: {outputZipPath}");
        }
    }