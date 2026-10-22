using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <xmlFilePath> <outputVisioPath>");
            return;
        }

        string inputVisioPath = args[0];
        string xmlFilePath = args[1];
        string outputVisioPath = args[2];

        // Guard against missing files.
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputVisioPath}");
            return;
        }

        if (!File.Exists(xmlFilePath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFilePath}");
            return;
        }

        try
        {
            // Read the XML content.
            string xmlContent = File.ReadAllText(xmlFilePath, Encoding.UTF8);

            // Compress the XML using GZip and encode as Base64 string.
            string compressedBase64 = CompressAndEncode(xmlContent);

            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // Create a new SolutionXML element.
                SolutionXML solutionXml = new SolutionXML();
                solutionXml.Name = Path.GetFileNameWithoutExtension(xmlFilePath);
                solutionXml.XmlValue = compressedBase64;

                // Add the compressed SolutionXML to the diagram.
                diagram.SolutionXMLs.Add(solutionXml);

                // Save the modified diagram as VSDX.
                diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram saved successfully with compressed SolutionXML.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Compresses the input string with GZip and returns a Base64‑encoded representation.
    /// </summary>
    private static string CompressAndEncode(string input)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        using (MemoryStream compressedStream = new MemoryStream())
        {
            // GZipStream with leaveOpen = true to access the underlying buffer after closing.
            using (GZipStream gzip = new GZipStream(compressedStream, CompressionMode.Compress, true))
            {
                gzip.Write(inputBytes, 0, inputBytes.Length);
            }

            // Convert compressed bytes to Base64 string for storage in SolutionXML.
            return Convert.ToBase64String(compressedStream.ToArray());
        }
    }
}