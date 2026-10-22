using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output ZIP file path (second argument or default)
        string outputZipPath = args.Length > 1 ? args[1] : "thumbnails.zip";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageCount = diagram.Pages.Count;

                // Create the ZIP archive for thumbnails
                using (FileStream zipFs = new FileStream(outputZipPath, FileMode.Create))
                using (ZipArchive zip = new ZipArchive(zipFs, ZipArchiveMode.Create))
                {
                    // Iterate over each page and generate a thumbnail
                    for (int i = 0; i < pageCount; i++)
                    {
                        // Configure image export options for a single page thumbnail
                        ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                        options.PageIndex = i;          // Export only the current page
                        options.PageCount = 1;          // Single page
                        options.Scale = 0.2f;           // Reduce size for thumbnail (20%)

                        // Temporary file to hold the exported image
                        string tempImagePath = Path.Combine(Path.GetTempPath(),
                            $"page_{i + 1}_{Guid.NewGuid()}.png");

                        // Export the page to the temporary PNG file
                        diagram.Save(tempImagePath, options);

                        // Add the PNG file to the ZIP archive
                        string entryName = $"page_{i + 1}.png";
                        ZipArchiveEntry entry = zip.CreateEntry(entryName);
                        using (FileStream imgFs = new FileStream(tempImagePath, FileMode.Open, FileAccess.Read))
                        using (Stream entryStream = entry.Open())
                        {
                            imgFs.CopyTo(entryStream);
                        }

                        // Clean up the temporary image file
                        File.Delete(tempImagePath);
                    }
                }
            }

            Console.WriteLine($"Thumbnails successfully saved to: {outputZipPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}