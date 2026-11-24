using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Determine output path (same folder with suffix "_compressed.vsdx").
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_compressed.vsdx");

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all SolutionXML elements.
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    if (string.IsNullOrEmpty(solXml.XmlValue))
                        continue; // Skip empty entries.

                    // Convert the XML string to UTF-8 bytes.
                    byte[] originalBytes = System.Text.Encoding.UTF8.GetBytes(solXml.XmlValue);

                    // Compress the bytes using GZip.
                    using (MemoryStream compressedStream = new MemoryStream())
                    {
                        using (GZipStream gzip = new GZipStream(compressedStream, CompressionMode.Compress, leaveOpen: true))
                        {
                            gzip.Write(originalBytes, 0, originalBytes.Length);
                        }

                        // Get the compressed byte array.
                        byte[] compressedBytes = compressedStream.ToArray();

                        // Store the compressed data as a Base64 string in XmlValue.
                        solXml.XmlValue = Convert.ToBase64String(compressedBytes);
                    }
                }

                // Save the modified diagram to a new VSDX file.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}