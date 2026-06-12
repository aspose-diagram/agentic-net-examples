using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioShapeEpsExporter
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string visioFilePath = @"C:\Path\To\InputDiagram.vsdx";

            // Output ZIP file path
            string zipFilePath = @"C:\Path\To\ShapesEpsCollection.zip";

            // Temporary folder to store individual EPS files
            string tempFolder = Path.Combine(Path.GetTempPath(), "VisioShapeEps_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempFolder);

            // Load the Visio diagram using the provided constructor
            using (Diagram diagram = new Diagram(visioFilePath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Build a unique file name for each shape (using Shape ID)
                        string epsFilePath = Path.Combine(tempFolder, $"shape_{shape.ID}.eps");

                        // Create image save options – EMF is the closest vector format supported
                        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Emf);

                        // Export the shape to an EMF file; the file extension is set to .eps
                        shape.ToImage(epsFilePath, saveOptions);
                    }
                }
            }

            // Create a ZIP archive containing all generated EPS files
            using (FileStream zipStream = new FileStream(zipFilePath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
                {
                    foreach (string file in Directory.GetFiles(tempFolder, "*.eps"))
                    {
                        archive.CreateEntryFromFile(file, Path.GetFileName(file));
                    }
                }
            }

            // Clean up temporary files
            Directory.Delete(tempFolder, true);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
