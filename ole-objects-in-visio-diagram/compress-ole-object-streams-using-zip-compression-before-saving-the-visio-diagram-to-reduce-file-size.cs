using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_compressed.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Check if the shape contains an OLE object (ForeignData)
                        if (shape.ForeignData != null && shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                        {
                            // Compress the OLE binary data using GZip
                            byte[] originalData = shape.ForeignData.ObjectData;
                            byte[] compressedData = CompressData(originalData);

                            // Replace the original OLE data with the compressed version
                            shape.ForeignData.ObjectData = compressedData;
                        }
                    }
                }

                // Save the modified diagram with the default VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Compresses a byte array using GZip compression.
    /// </summary>
    /// <param name="data">The original uncompressed data.</param>
    /// <returns>The compressed byte array.</returns>
    private static byte[] CompressData(byte[] data)
    {
        using (MemoryStream output = new MemoryStream())
        {
            // GZipStream with leaveOpen = true to ensure the MemoryStream remains open after disposing the GZipStream
            using (GZipStream gzip = new GZipStream(output, CompressionMode.Compress, true))
            {
                gzip.Write(data, 0, data.Length);
            }
            return output.ToArray();
        }
    }
}