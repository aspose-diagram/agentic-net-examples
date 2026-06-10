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

            // Path where the zip archive with thumbnails will be saved
            string zipPath = "thumbnails.zip";

            // Load the diagram using the provided constructor
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Create the zip archive (will be created anew)
                using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
                {
                    int pageCount = diagram.Pages.Count;

                    // Iterate through each page in the diagram
                    for (int i = 0; i < pageCount; i++)
                    {
                        // Configure image save options for a single page
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                        {
                            PageIndex = i,   // zero‑based index of the page to render
                            PageCount = 1    // render only this page
                        };

                        // Render the page to a memory stream using the provided Save method
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            diagram.Save(imgStream, imgOptions);
                            imgStream.Position = 0; // reset stream position for reading

                            // Create an entry in the zip archive for this thumbnail
                            string entryName = $"page_{i + 1}.png";
                            ZipArchiveEntry entry = archive.CreateEntry(entryName, CompressionLevel.Optimal);

                            // Write the image data into the zip entry
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
