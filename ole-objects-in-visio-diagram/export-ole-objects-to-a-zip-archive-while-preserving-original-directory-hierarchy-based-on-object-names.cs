using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) and output ZIP file path (second argument)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
        string zipPath = args.Length > 1 ? args[1] : "OleObjects.zip";

        // Guard against missing input file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Create (or overwrite) the ZIP archive
            using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Create, FileAccess.Write))
            using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create, leaveOpen: false))
            {
                // Iterate through all pages and shapes to find OLE objects
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains foreign (OLE) data
                        if (shape.ForeignData != null && shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                        {
                            // Determine a file name based on the original source path of the OLE object
                            string sourceFullName = shape.ForeignData.ObjectSourceFullName;

                            // Fallback to shape name if source path is unavailable
                            if (string.IsNullOrEmpty(sourceFullName))
                            {
                                sourceFullName = $"{shape.NameU}_{shape.ID}";
                            }

                            // Normalize the path for ZIP entry (use forward slashes)
                            string entryPath = sourceFullName.Replace('\\', '/').TrimStart('/');

                            // Ensure the entry path does not contain invalid characters for ZIP entries
                            foreach (char invalidChar in Path.GetInvalidPathChars())
                            {
                                entryPath = entryPath.Replace(invalidChar.ToString(), "_");
                            }

                            // Create a new entry in the ZIP archive
                            ZipArchiveEntry entry = zipArchive.CreateEntry(entryPath, CompressionLevel.Optimal);

                            // Write the OLE binary data into the ZIP entry
                            using (Stream entryStream = entry.Open())
                            {
                                entryStream.Write(shape.ForeignData.ObjectData, 0, shape.ForeignData.ObjectData.Length);
                            }

                            Console.WriteLine($"Exported OLE object to '{entryPath}'.");
                        }
                    }
                }
            }

            Console.WriteLine($"All OLE objects have been exported to ZIP archive: {zipPath}");
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram operations or I/O
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}