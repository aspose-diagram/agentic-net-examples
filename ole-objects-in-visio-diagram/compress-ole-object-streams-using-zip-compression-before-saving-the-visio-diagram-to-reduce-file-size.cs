using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output Visio file path
        string outputPath = "output_compressed.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate over all shapes on the page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Check if the shape contains foreign (OLE) data
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                    {
                        byte[] originalData = shape.ForeignData.ObjectData;

                        // Compress the OLE data using GZip (ZIP compression)
                        using (MemoryStream compressedStream = new MemoryStream())
                        {
                            using (GZipStream gzip = new GZipStream(compressedStream, CompressionLevel.Optimal, leaveOpen: true))
                            {
                                gzip.Write(originalData, 0, originalData.Length);
                            }

                            // Get the compressed byte array
                            byte[] compressedData = compressedStream.ToArray();

                            // Replace the original OLE data with the compressed version
                            shape.ForeignData.ObjectData = compressedData;
                        }
                    }
                }
            }

            // Save the modified diagram with compression applied to OLE objects
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}