using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: first argument is output ZIP path, remaining are input Visio files
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <output-zip-path> <input-visio-1> [<input-visio-2> ...]");
            return;
        }

        string outputZipPath = args[0];
        string[] inputFiles = new string[args.Length - 1];
        Array.Copy(args, 1, inputFiles, 0, inputFiles.Length);

        // Verify each input file exists
        foreach (string path in inputFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // Create (or overwrite) the ZIP archive
        try
        {
            using (FileStream zipToOpen = new FileStream(outputZipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
            {
                // Process each Visio file
                foreach (string visioPath in inputFiles)
                {
                    try
                    {
                        // Load the Visio diagram inside a using block as required
                        using (Diagram diagram = new Diagram(visioPath))
                        {
                            int pageIndex = 0; // Manual page counter because Page has no Index property

                            // Iterate through all pages
                            foreach (Page page in diagram.Pages)
                            {
                                pageIndex++;

                                // Iterate through all shapes on the page
                                foreach (Shape shape in page.Shapes)
                                {
                                    // Skip deleted shapes
                                    if (shape.Del == BOOL.True)
                                        continue;

                                    // Prepare image save options (PNG format)
                                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);

                                    // Export shape to a memory stream
                                    using (MemoryStream imgStream = new MemoryStream())
                                    {
                                        // Export the shape image
                                        shape.ToImage(imgStream, imgOptions);
                                        imgStream.Position = 0;

                                        // Build a unique entry name for the ZIP archive
                                        string entryName = $"{Path.GetFileNameWithoutExtension(visioPath)}_Page{pageIndex}_Shape{shape.ID}.png";

                                        // Add the image to the ZIP archive
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
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error processing file '{visioPath}': {ex.Message}");
                        // Continue with next file
                    }
                }
            }

            Console.WriteLine($"Images extracted and saved to ZIP archive: {outputZipPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create ZIP archive: {ex.Message}");
        }
    }
}