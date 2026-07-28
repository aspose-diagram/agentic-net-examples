using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Aspose.Diagram;

class CompressSolutionXml
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination VSDX files
            string sourcePath = "input.vsdx";
            string destinationPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(sourcePath);

            // Iterate through all SolutionXML objects in the diagram
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                // Ensure there is XML content to compress
                if (!string.IsNullOrEmpty(solXml.XmlValue))
                {
                    // Convert the XML string to UTF‑8 bytes
                    byte[] originalBytes = Encoding.UTF8.GetBytes(solXml.XmlValue);

                    // Compress the bytes using GZip
                    using (MemoryStream compressedStream = new MemoryStream())
                    {
                        using (GZipStream gzip = new GZipStream(compressedStream, CompressionMode.Compress, true))
                        {
                            gzip.Write(originalBytes, 0, originalBytes.Length);
                        }

                        // Get the compressed byte array
                        byte[] compressedBytes = compressedStream.ToArray();

                        // Store the compressed data as a Base64 string back into XmlValue
                        solXml.XmlValue = Convert.ToBase64String(compressedBytes);
                    }
                }
            }

            // Save the modified diagram as a VSDX package
            diagram.Save(destinationPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
