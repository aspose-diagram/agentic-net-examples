using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string visioPath = "input.vsdx";

        // Guard: ensure the Visio file exists before proceeding
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Output ZIP file path
        string zipPath = "OleObjects.zip";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Create or overwrite the ZIP archive
            using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
            using (ZipArchive zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape is a foreign OLE object with binary data
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object &&
                            shape.ForeignData.ObjectData != null)
                        {
                            // Determine a name for the OLE object:
                            // Use the shape's NameU if it is not empty; otherwise fallback to the shape ID
                            string objectName = !string.IsNullOrWhiteSpace(shape.NameU)
                                ? shape.NameU
                                : $"Object_{shape.ID}";

                            // Normalize the name to use forward slashes for ZIP entry paths
                            string entryPath = objectName.Replace('\\', '/').TrimStart('/');

                            // Ensure the entry path is not empty
                            if (string.IsNullOrEmpty(entryPath))
                            {
                                entryPath = $"Object_{shape.ID}";
                            }

                            // Create a new entry in the ZIP archive
                            ZipArchiveEntry entry = zipArchive.CreateEntry(entryPath, CompressionLevel.Optimal);

                            // Write the OLE binary data to the entry
                            using (Stream entryStream = entry.Open())
                            {
                                entryStream.Write(shape.ForeignData.ObjectData, 0, shape.ForeignData.ObjectData.Length);
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"OLE objects have been exported to '{zipPath}'.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}