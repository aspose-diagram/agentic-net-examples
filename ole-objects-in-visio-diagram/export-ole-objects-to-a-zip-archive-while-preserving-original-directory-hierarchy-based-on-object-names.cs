using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output ZIP file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputZipPath>");
            return;
        }

        string inputVisioPath = args[0];
        string outputZipPath = args[1];

        // Guard: verify input Visio file exists
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputVisioPath}");
            return;
        }

        // Guard: ensure the directory for the output ZIP exists
        string outputDir = Path.GetDirectoryName(outputZipPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputVisioPath);

            // Create the ZIP archive
            using (FileStream zipStream = new FileStream(outputZipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify OLE (foreign) shapes
                        if (shape.ForeignData != null &&
                            shape.ForeignData.ObjectData != null &&
                            shape.ForeignData.ObjectData.Length > 0)
                        {
                            // Determine a file name based on the source full name or shape name
                            string sourceFullName = shape.ForeignData.ObjectSourceFullName;
                            string fileName = !string.IsNullOrEmpty(sourceFullName)
                                ? Path.GetFileName(sourceFullName)
                                : $"{shape.NameU}.bin";

                            // Determine relative directory hierarchy (if any)
                            string relativePath = null;
                            if (!string.IsNullOrEmpty(sourceFullName))
                            {
                                string sourceDir = Path.GetDirectoryName(sourceFullName);
                                if (!string.IsNullOrEmpty(sourceDir))
                                {
                                    // Remove drive/root part to make it relative
                                    string root = Path.GetPathRoot(sourceDir);
                                    if (!string.IsNullOrEmpty(root) && sourceDir.StartsWith(root, StringComparison.OrdinalIgnoreCase))
                                    {
                                        sourceDir = sourceDir.Substring(root.Length);
                                    }

                                    // Normalize to forward slashes for ZIP entries
                                    relativePath = sourceDir.Replace('\\', '/').TrimStart('/');
                                }
                            }

                            // Build the full entry path inside the ZIP
                            string entryPath = string.IsNullOrEmpty(relativePath)
                                ? fileName
                                : $"{relativePath}/{fileName}";

                            // Create the ZIP entry and write the OLE data
                            ZipArchiveEntry entry = archive.CreateEntry(entryPath, CompressionLevel.Optimal);
                            using (Stream entryStream = entry.Open())
                            using (MemoryStream ms = new MemoryStream(shape.ForeignData.ObjectData))
                            {
                                ms.CopyTo(entryStream);
                            }

                            Console.WriteLine($"Exported OLE object to ZIP entry: {entryPath}");
                        }
                    }
                }
            }

            Console.WriteLine($"OLE objects have been successfully exported to: {outputZipPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}