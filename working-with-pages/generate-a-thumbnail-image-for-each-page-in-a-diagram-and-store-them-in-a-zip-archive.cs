using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramThumbnailsToZip
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string visioFilePath = @"C:\Input\sample.vsdx";

            // Output ZIP file path that will contain the thumbnails
            string zipFilePath = @"C:\Output\thumbnails.zip";

            // Load the diagram using the provided constructor (lifecycle rule)
            using (Diagram diagram = new Diagram(visioFilePath))
            {
                // Create a ZIP archive for the thumbnails
                using (FileStream zipStream = new FileStream(zipFilePath, FileMode.Create))
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
                {
                    // Iterate through each page in the diagram
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        // Configure image save options for the current page
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                        {
                            PageIndex = i,          // 0‑based index of the page to render
                            PageCount = 1,          // Render only this page
                            Resolution = 96,        // DPI (optional, adjust as needed)
                            // Additional options can be set here if required
                        };

                        // Render the page to a memory stream
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            // Save the diagram page as an image using the provided Save overload (lifecycle rule)
                            diagram.Save(imgStream, imgOptions);
                            imgStream.Seek(0, SeekOrigin.Begin);

                            // Create an entry in the ZIP archive for this thumbnail
                            string entryName = $"page_{i + 1}.png";
                            ZipArchiveEntry entry = archive.CreateEntry(entryName, CompressionLevel.Optimal);

                            // Write the image data into the ZIP entry
                            using (Stream entryStream = entry.Open())
                            {
                                imgStream.CopyTo(entryStream);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Thumbnails have been generated and stored in the ZIP archive.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
