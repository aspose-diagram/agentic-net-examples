using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;

class ExportOleObjectsToZip
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file
            string diagramPath = @"C:\Diagrams\sample.vsdx";

            // Path where the resulting zip archive will be saved
            string zipOutputPath = @"C:\Exports\OleObjects.zip";

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Create a memory stream to hold the zip archive
            using (MemoryStream zipStream = new MemoryStream())
            {
                // Initialize the zip archive for creation
                using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Check if the shape contains foreign data (OLE object)
                            if (shape.ForeignData != null && !string.IsNullOrEmpty(shape.ForeignData.ObjectSourceFullName))
                            {
                                // Full path of the source file for the linked OLE object
                                string sourceFilePath = shape.ForeignData.ObjectSourceFullName;

                                // Ensure the source file exists before attempting to read it
                                if (File.Exists(sourceFilePath))
                                {
                                    // Read the OLE object's file bytes
                                    byte[] fileBytes = File.ReadAllBytes(sourceFilePath);

                                    // Build a hierarchical entry name based on the shape name
                                    // Replace any invalid path characters to avoid zip entry errors
                                    string safeShapeName = string.Join("_", shape.Name.Split(Path.GetInvalidFileNameChars()));
                                    string fileName = Path.GetFileName(sourceFilePath);
                                    string entryPath = $"{safeShapeName}/{fileName}";

                                    // Create the entry in the zip archive and write the file bytes
                                    ZipArchiveEntry entry = zip.CreateEntry(entryPath, CompressionLevel.Optimal);
                                    using (Stream entryStream = entry.Open())
                                    {
                                        entryStream.Write(fileBytes, 0, fileBytes.Length);
                                    }
                                }
                            }
                        }
                    }
                }

                // Write the zip archive from the memory stream to the output file
                using (FileStream fileStream = new FileStream(zipOutputPath, FileMode.Create, FileAccess.Write))
                {
                    zipStream.Seek(0, SeekOrigin.Begin);
                    zipStream.CopyTo(fileStream);
                }
            }

            Console.WriteLine($"OLE objects have been exported to '{zipOutputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
