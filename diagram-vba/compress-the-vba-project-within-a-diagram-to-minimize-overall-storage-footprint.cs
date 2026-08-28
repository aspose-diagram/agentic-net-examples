using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string sourcePath = "source.vsdx";
            string destinationPath = "compressed.vsdx";

            // Load the diagram from the source file
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // Retrieve the existing VBA project data (MIME‑encoded byte array)
                byte[] originalVbData = diagram.VbProjectData;

                // If VBA data exists, compress it to reduce storage size
                if (originalVbData != null && originalVbData.Length > 0)
                {
                    using (MemoryStream compressedStream = new MemoryStream())
                    {
                        // GZipStream performs the actual compression
                        using (GZipStream gzip = new GZipStream(compressedStream, CompressionLevel.Optimal, leaveOpen: true))
                        {
                            gzip.Write(originalVbData, 0, originalVbData.Length);
                        }

                        // Assign the compressed byte array back to the diagram
                        diagram.VbProjectData = compressedStream.ToArray();
                    }
                }

                // Save the modified diagram to the destination file
                diagram.Save(destinationPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
