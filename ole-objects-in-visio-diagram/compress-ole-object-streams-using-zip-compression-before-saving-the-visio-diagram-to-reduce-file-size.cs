using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CompressOleObjects
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains foreign (OLE) data
                        ForeignData foreign = shape.ForeignData;
                        if (foreign != null && foreign.ObjectData != null && foreign.ObjectData.Length > 0)
                        {
                            // Compress the OLE object data using ZIP (GZip) compression
                            byte[] originalData = foreign.ObjectData;
                            byte[] compressedData;

                            using (MemoryStream ms = new MemoryStream())
                            {
                                // GZipStream provides ZIP (deflate) compression
                                using (GZipStream gzip = new GZipStream(ms, CompressionLevel.Optimal, leaveOpen: true))
                                {
                                    gzip.Write(originalData, 0, originalData.Length);
                                }
                                compressedData = ms.ToArray();
                            }

                            // Replace the original OLE data with the compressed version
                            foreign.ObjectData = compressedData;
                        }
                    }
                }

                // Save the modified diagram to a new file (same format as original)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
