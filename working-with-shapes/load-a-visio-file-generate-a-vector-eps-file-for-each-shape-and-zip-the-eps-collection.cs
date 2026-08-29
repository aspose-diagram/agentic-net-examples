using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioEpsExporter
{
    // Exports each shape of a Visio file to an EPS (vector) file and packs them into a zip archive.
    public static void ExportShapesToEpsZip(string visioFilePath, string outputZipPath)
    {
        // Load the Visio diagram using the Diagram(string) constructor (lifecycle rule).
        using (Diagram diagram = new Diagram(visioFilePath))
        {
            // Prepare a temporary directory to hold the EPS files.
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);

            // Configure image save options for a vector format (EMF). 
            // EPS is not directly supported; EMF is a vector format and we save with .eps extension.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Emf);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Build a unique file name for each shape.
                    string epsFileName = $"Page{page.ID}_Shape{shape.ID}.eps";
                    string epsFilePath = Path.Combine(tempDir, epsFileName);

                    // Export the shape to an image file using the ToImage method (rule).
                    shape.ToImage(epsFilePath, saveOptions);
                }
            }

            // Create the zip archive containing all EPS files.
            using (FileStream zipToCreate = new FileStream(outputZipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
            {
                foreach (string filePath in Directory.GetFiles(tempDir, "*.eps"))
                {
                    // Add each EPS file to the zip archive.
                    archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                }
            }

            // Clean up temporary files.
            Directory.Delete(tempDir, true);
        }
    }

    // Example usage.
    static void Main()
    {
        try
        {

            string visioPath = @"C:\Input\sample.vsdx";
            string zipPath   = @"C:\Output\shapes_eps.zip";

            ExportShapesToEpsZip(visioPath, zipPath);
            Console.WriteLine("Export completed.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
