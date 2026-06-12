using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram (or load an existing one)
        Diagram diagram = new Diagram();

        // Example XML content to be stored
        string originalXml = "<root><data>Example</data></root>";

        // Compress the XML using GZip
        byte[] compressedBytes;
        using (MemoryStream ms = new MemoryStream())
        {
            using (GZipStream gzip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                byte[] xmlBytes = Encoding.UTF8.GetBytes(originalXml);
                gzip.Write(xmlBytes, 0, xmlBytes.Length);
            }
            compressedBytes = ms.ToArray();
        }

        // Encode compressed data as Base64 string (SolutionXML expects a string)
        string compressedBase64 = Convert.ToBase64String(compressedBytes);

        // Create a SolutionXML object with the compressed data
        SolutionXML solutionXml = new SolutionXML("CompressedData", compressedBase64);

        // Add the SolutionXML to the diagram's collection
        diagram.SolutionXMLs.Add(solutionXml);

        // Save the diagram as a VSDX package
        diagram.Save("CompressedSolution.vsdx", SaveFileFormat.Vsdx);
    }
}
