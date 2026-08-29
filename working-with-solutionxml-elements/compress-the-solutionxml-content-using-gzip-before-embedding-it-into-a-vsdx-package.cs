using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing VSDX diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all SolutionXML entries and replace their content with GZip‑compressed data
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                // Original XML string
                string originalXml = solXml.XmlValue;

                // Compress the XML string to a byte array
                byte[] compressedBytes = CompressStringToGzip(originalXml);

                // Store the compressed data as a Base64 string (SolutionXML expects a string)
                solXml.XmlValue = Convert.ToBase64String(compressedBytes);
            }

            // Save the modified diagram back to VSDX format
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Compresses a UTF‑8 string using GZip and returns the resulting byte array
    private static byte[] CompressStringToGzip(string text)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(text);
        using (var outputStream = new MemoryStream())
        {
            using (var gzipStream = new GZipStream(outputStream, CompressionMode.Compress, leaveOpen: true))
            {
                gzipStream.Write(inputBytes, 0, inputBytes.Length);
            }
            // GZipStream must be closed before reading the compressed data
            return outputStream.ToArray();
        }
    }
}
