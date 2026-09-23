using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input diagram file path and output zip file path
            string diagramPath = @"C:\Diagrams\sample.vsdx";
            string zipPath = @"C:\Diagrams\thumbnails.zip";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Create a zip archive for the thumbnails
            using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Create))
            using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
            {
                // Iterate through each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure image save options for the current page
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        PageIndex = i,   // zero‑based page index
                        PageCount = 1    // export only this page
                    };

                    // Save the page to a memory stream
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        diagram.Save(imageStream, saveOptions);
                        imageStream.Position = 0; // reset stream position for reading

                        // Add the image as an entry in the zip archive
                        string entryName = $"Page_{i + 1}.png"; // human‑readable page number
                        ZipArchiveEntry entry = zipArchive.CreateEntry(entryName, CompressionLevel.Optimal);
                        using (Stream entryStream = entry.Open())
                        {
                            imageStream.CopyTo(entryStream);
                        }
                    }
                }
            }

            // Optional: inform the user
            Console.WriteLine($"Thumbnails for {diagram.Pages.Count} pages saved to '{zipPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
