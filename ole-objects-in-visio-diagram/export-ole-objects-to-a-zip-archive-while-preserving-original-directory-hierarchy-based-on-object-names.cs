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
                Console.WriteLine("Usage: OleExportToZip <inputVisioFile> <outputZipFile>");
                return;
            }

            string inputVisioPath = args[0];
            string outputZipPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputVisioPath);

            // Create the ZIP archive for OLE objects
            using (FileStream zipStream = new FileStream(outputZipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape is an OLE object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Ensure there is binary data to export
                            byte[] oleData = shape.ForeignData.ObjectData;
                            if (oleData == null || oleData.Length == 0)
                                continue;

                            // Determine a folder name based on the shape's universal name (NameU)
                            // If NameU is empty, use a generic placeholder
                            string folderName = string.IsNullOrWhiteSpace(shape.NameU) ? "UnnamedShape" : shape.NameU.Trim();

                            // Build the entry path: <folder>/<shapeId>.bin
                            string entryPath = $"{folderName}/{shape.ID}.bin";

                            // Create the entry in the ZIP archive
                            ZipArchiveEntry entry = archive.CreateEntry(entryPath, CompressionLevel.Optimal);

                            // Write the OLE binary data to the entry
                            using (Stream entryStream = entry.Open())
                            {
                                entryStream.Write(oleData, 0, oleData.Length);
                            }

                            Console.WriteLine($"Exported OLE object from shape ID {shape.ID} to '{entryPath}'.");
                        }
                    }
                }
            }

            Console.WriteLine($"OLE objects have been exported to ZIP file: {outputZipPath}");
        }
    }