using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CompressVbaProject
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string inputPath = "input.vsdx";

            // Path where the compressed diagram will be saved
            string outputPath = "output.vsdx";

            // Load the diagram using the provided constructor
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the VBA project data (MIME‑encoded byte array)
                byte[] vbData = diagram.VbProjectData;

                // If VBA data exists, compress it with GZip
                if (vbData != null && vbData.Length > 0)
                {
                    using (MemoryStream sourceStream = new MemoryStream(vbData))
                    using (MemoryStream compressedStream = new MemoryStream())
                    {
                        // GZip compression
                        using (GZipStream gzip = new GZipStream(compressedStream, CompressionMode.Compress, true))
                        {
                            sourceStream.CopyTo(gzip);
                        }

                        // Replace the original VBA data with the compressed version
                        diagram.VbProjectData = compressedStream.ToArray();
                    }
                }

                // Save the diagram using the provided Save method
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
