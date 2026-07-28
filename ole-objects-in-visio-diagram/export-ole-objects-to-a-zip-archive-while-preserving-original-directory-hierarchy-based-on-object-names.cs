using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;

class OleObjectsExporter
{
    // Exports linked OLE objects from a Visio diagram to a zip archive.
    // The original directory hierarchy (as indicated by the OLE object's source file name) is preserved.
    public static void ExportOleObjectsToZip(string visioFilePath, string zipOutputPath)
    {
        // Load the Visio diagram (uses Aspose.Diagram's loading mechanism)
        Diagram diagram = new Diagram(visioFilePath);

        // Create the zip archive for output
        using (FileStream zipStream = new FileStream(zipOutputPath, FileMode.Create))
        using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains foreign data (possible OLE object)
                    if (shape.ForeignData != null)
                    {
                        string sourceFullName = shape.ForeignData.ObjectSourceFullName;

                        // Only process linked OLE objects that have a source file path
                        if (!string.IsNullOrEmpty(sourceFullName) && File.Exists(sourceFullName))
                        {
                            // Preserve the original directory hierarchy inside the zip.
                            // Use the sourceFullName relative to its root (e.g., "C:\Folder\Sub\file.doc").
                            // We'll store the path relative to the drive root to keep hierarchy.
                            string entryPath = GetRelativePathForZip(sourceFullName);

                            // Create a new entry in the zip archive
                            ZipArchiveEntry entry = zip.CreateEntry(entryPath, CompressionLevel.Optimal);

                            // Write the file content into the zip entry
                            using (Stream entryStream = entry.Open())
                            using (FileStream sourceStream = new FileStream(sourceFullName, FileMode.Open, FileAccess.Read))
                            {
                                sourceStream.CopyTo(entryStream);
                            }
                        }
                    }
                }
            }
        }
    }

    // Converts an absolute file path to a relative path suitable for zip entry storage.
    // Example: "C:\Data\Docs\file.doc" -> "Data/Docs/file.doc"
    private static string GetRelativePathForZip(string absolutePath)
    {
        // Remove drive letter and colon, replace backslashes with forward slashes.
        string pathWithoutDrive = absolutePath;

        // If path starts with a drive letter (e.g., "C:\")
        if (Path.IsPathRooted(absolutePath))
        {
            // Get the root (e.g., "C:\") and trim it
            string root = Path.GetPathRoot(absolutePath);
            pathWithoutDrive = absolutePath.Substring(root.Length);
        }

        // Normalize separators for zip entries
        return pathWithoutDrive.Replace('\\', '/');
    }

    // Example usage
    static void Main()
    {
        try
        {

            string visioFile = @"C:\Diagrams\sample.vsdx";
            string zipFile = @"C:\ExportedOleObjects\ole_objects.zip";

            ExportOleObjectsToZip(visioFile, zipFile);

            Console.WriteLine("OLE objects have been exported to zip archive.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
