using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioImagesToZip
{
    static void Main()
    {
        try
        {

            // Paths to the Visio files to process
            string[] visioFiles = new string[]
            {
                @"C:\Visio\Diagram1.vsdx",
                @"C:\Visio\Diagram2.vsdx"
                // add more file paths as needed
            };

            // Output ZIP file path
            string outputZipPath = @"C:\Visio\ShapeImages.zip";

            // Create the ZIP archive
            using (FileStream zipFileStream = new FileStream(outputZipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
            {
                foreach (string visioPath in visioFiles)
                {
                    // Load the Visio document using the provided constructor rule
                    using (Diagram diagram = new Diagram(visioPath))
                    {
                        int pageIndex = 0;
                        foreach (Page page in diagram.Pages)
                        {
                            int shapeIndex = 0;
                            foreach (Shape shape in page.Shapes)
                            {
                                // Build a unique entry name for each shape image
                                string entryName = $"{Path.GetFileNameWithoutExtension(visioPath)}_Page{pageIndex}_Shape{shapeIndex}.png";

                                // Render the shape to a memory stream as PNG
                                using (MemoryStream imageStream = new MemoryStream())
                                {
                                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                                    shape.ToImage(imageStream, imgOptions);
                                    imageStream.Position = 0;

                                    // Add the image stream to the ZIP archive
                                    ZipArchiveEntry entry = archive.CreateEntry(entryName);
                                    using (Stream entryStream = entry.Open())
                                    {
                                        imageStream.CopyTo(entryStream);
                                    }
                                }

                                shapeIndex++;
                            }
                            pageIndex++;
                        }
                    } // diagram disposed here
                }
            }

            Console.WriteLine("All shape images have been extracted to: " + outputZipPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
