using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through every page and every shape on each page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // If the shape contains embedded OLE object data, compress it
                    if (shape.ForeignData != null &&
                        shape.ForeignData.ObjectData != null &&
                        shape.ForeignData.ObjectData.Length > 0)
                    {
                        byte[] originalData = shape.ForeignData.ObjectData;
                        byte[] compressedData;

                        // Compress using ZIP (Deflate) compression
                        using (MemoryStream compressedStream = new MemoryStream())
                        {
                            using (DeflateStream zipStream = new DeflateStream(compressedStream, CompressionLevel.Optimal, leaveOpen: true))
                            {
                                zipStream.Write(originalData, 0, originalData.Length);
                            }
                            compressedData = compressedStream.ToArray();
                        }

                        // Replace the original OLE data with the compressed version
                        shape.ForeignData.ObjectData = compressedData;
                    }
                }
            }

            // Save the diagram with the compressed OLE streams
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
