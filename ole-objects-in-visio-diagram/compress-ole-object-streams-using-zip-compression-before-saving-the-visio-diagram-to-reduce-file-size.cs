using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output_compressed.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only foreign (OLE) shapes that contain object data
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object &&
                            shape.ForeignData.ObjectData != null &&
                            shape.ForeignData.ObjectData.Length > 0)
                        {
                            // Compress the OLE object data using ZIP
                            byte[] originalData = shape.ForeignData.ObjectData;
                            byte[] compressedData;

                            using (MemoryStream compressedStream = new MemoryStream())
                            {
                                using (ZipArchive zip = new ZipArchive(compressedStream, ZipArchiveMode.Create, true))
                                {
                                    // Create a single entry to hold the OLE data
                                    ZipArchiveEntry entry = zip.CreateEntry("oledata");
                                    using (Stream entryStream = entry.Open())
                                    {
                                        entryStream.Write(originalData, 0, originalData.Length);
                                    }
                                }

                                // Get the compressed byte array
                                compressedData = compressedStream.ToArray();
                            }

                            // Replace the original OLE data with the compressed version
                            shape.ForeignData.ObjectData = compressedData;
                        }
                    }
                }

                // Save the modified diagram (using VSDX format as an example)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved with compressed OLE streams to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }