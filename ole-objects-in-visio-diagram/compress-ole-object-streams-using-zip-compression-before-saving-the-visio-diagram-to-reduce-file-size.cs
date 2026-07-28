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
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram (lifecycle rule)
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to locate OLE objects
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // ForeignData holds embedded OLE or image data
                    ForeignData foreign = shape.ForeignData;
                    if (foreign == null)
                        continue;

                    // Compress embedded OLE object data, if present
                    if (foreign.ObjectData != null && foreign.ObjectData.Length > 0)
                    {
                        foreign.ObjectData = CompressBytes(foreign.ObjectData);
                    }

                    // Compress image data of the foreign object, if present
                    if (foreign.ImageData != null && foreign.ImageData.Length > 0)
                    {
                        foreign.ImageData = CompressBytes(foreign.ImageData);
                    }
                }
            }

            // Save the modified diagram (lifecycle rule)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method that applies ZIP (GZip) compression to a byte array
    private static byte[] CompressBytes(byte[] data)
    {
        using (var output = new MemoryStream())
        {
            // Use GZipStream with optimal compression level (ZIP compression)
            using (var gzip = new GZipStream(output, CompressionLevel.Optimal, leaveOpen: true))
            {
                gzip.Write(data, 0, data.Length);
            }
            return output.ToArray();
        }
    }
}
