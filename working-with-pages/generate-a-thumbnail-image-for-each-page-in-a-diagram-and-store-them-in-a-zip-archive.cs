using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputZipPath>");
            return;
        }

        string inputPath = args[0];
        string outputZipPath = args[1];

        // Guard: ensure the input diagram file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create the ZIP archive for thumbnails.
                using (FileStream zipStream = new FileStream(outputZipPath, FileMode.Create))
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
                {
                    // Iterate over each page in the diagram.
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        // Configure image export options for a single page.
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        imgOptions.PageIndex = i;      // Zero‑based page index.
                        imgOptions.PageCount = 1;      // Export only this page.

                        // Export the page to a memory stream.
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            diagram.Save(imgStream, imgOptions);
                            imgStream.Position = 0; // Reset stream position for reading.

                            // Create a ZIP entry for the thumbnail.
                            string entryName = $"page_{i + 1}.png";
                            ZipArchiveEntry entry = archive.CreateEntry(entryName, CompressionLevel.Optimal);
                            using (Stream entryStream = entry.Open())
                            {
                                imgStream.CopyTo(entryStream);
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Thumbnails successfully saved to: {outputZipPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}