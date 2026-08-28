using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramThumbnailsToZip
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string diagramPath = "input.vsdx";

            // Path where the zip archive with thumbnails will be created
            string zipPath = "thumbnails.zip";

            // Load the diagram using the constructor that accepts a file name
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Create the zip archive for writing
                using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
                {
                    // Iterate through all pages in the diagram
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        // Configure image save options for the current page
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                        {
                            PageIndex = i,      // zero‑based index of the page to render
                            PageCount = 1,      // render only this page
                            Resolution = 96     // optional: set DPI for the thumbnail
                        };

                        // Render the page to a memory stream
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            diagram.Save(imgStream, imgOptions);
                            imgStream.Seek(0, SeekOrigin.Begin);

                            // Add the image as an entry in the zip archive
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

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
