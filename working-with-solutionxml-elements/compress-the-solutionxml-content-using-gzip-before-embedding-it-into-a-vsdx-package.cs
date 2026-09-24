using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Aspose.Diagram;

class Program
{
    // Compress a string using GZip and return Base64 representation
    private static string CompressString(string text)
    {
        byte[] rawBytes = Encoding.UTF8.GetBytes(text);
        using (var compressedStream = new MemoryStream())
        {
            using (var gzip = new GZipStream(compressedStream, CompressionMode.Compress, leaveOpen: true))
            {
                gzip.Write(rawBytes, 0, rawBytes.Length);
            }
            // GZipStream is disposed, now get the compressed bytes
            return Convert.ToBase64String(compressedStream.ToArray());
        }
    }

    static void Main()
    {
        try
        {

            // Paths – adjust as needed
            string inputPath = "input.vsdx";
            string outputPath = "output_compressed.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all SolutionXML elements and replace their XmlValue with compressed data
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                if (!string.IsNullOrEmpty(solXml.XmlValue))
                {
                    string compressed = CompressString(solXml.XmlValue);
                    solXml.XmlValue = compressed;
                }
            }

            // Save the modified diagram back to VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
