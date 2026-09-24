using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (adjust as needed)
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Folder to store individual EPS (EMF) files
        string epsFolder = "ExportedEps";
        Directory.CreateDirectory(epsFolder);

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Prepare EPS (EMF) file name
                    string epsFileName = $"Shape_Page{page.ID}_Shape{shape.ID}.eps";
                    string epsFilePath = Path.Combine(epsFolder, epsFileName);

                    // Export the shape to EMF (vector) using ImageSaveOptions
                    // Note: Aspose.Diagram does not have native EPS support; EMF is used as a vector alternative.
                    ImageSaveOptions epsOptions = new ImageSaveOptions(SaveFileFormat.Emf);
                    shape.ToImage(epsFilePath, epsOptions);
                }
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Create a ZIP archive containing all EPS (EMF) files
        string zipPath = "ShapesEpsCollection.zip";
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
        {
            foreach (string epsFile in Directory.GetFiles(epsFolder, "*.eps"))
            {
                string entryName = Path.GetFileName(epsFile);
                archive.CreateEntryFromFile(epsFile, entryName);
            }
        }

        // Optional: clean up temporary EPS files
        // Directory.Delete(epsFolder, true);
    }
}