using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Folder containing Visio files
        string folderPath = @"C:\VisioFiles";

        // Guard to ensure the folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Process each supported Visio file in the folder
        foreach (string filePath in Directory.GetFiles(folderPath))
        {
            // Guard to ensure the file exists (defensive check)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (!IsSupportedVisioExtension(ext))
                continue;

            try
            {
                // Load the diagram from the file
                Diagram diagram = new Diagram(filePath);

                // Update each SolutionXML with a timestamp attribute
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    if (string.IsNullOrEmpty(solXml.XmlValue))
                        continue;

                    // Parse existing XML content
                    XDocument xDoc = XDocument.Parse(solXml.XmlValue);

                    // Add or update the timestamp attribute on the root element
                    XElement root = xDoc.Root;
                    if (root != null)
                    {
                        root.SetAttributeValue("timestamp", DateTime.UtcNow.ToString("o"));
                        // Save the modified XML back to the SolutionXML
                        solXml.XmlValue = xDoc.Declaration != null
                            ? xDoc.Declaration + xDoc.ToString(System.Xml.Linq.SaveOptions.DisableFormatting)
                            : xDoc.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
                    }
                }

                // Save the diagram back using the original format
                SaveFileFormat format = GetSaveFormatFromExtension(ext);
                diagram.Save(filePath, format);

                Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Report any errors encountered during processing
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }
    }

    // Determines if the file extension is a supported Visio format
    static bool IsSupportedVisioExtension(string ext)
    {
        return ext == ".vsdx" || ext == ".vsd" || ext == ".vdx" ||
               ext == ".vsx" || ext == ".vtx" || ext == ".vssx" ||
               ext == ".vstx" || ext == ".vsdm" || ext == ".vssm" ||
               ext == ".vstm";
    }

    // Maps file extension to the corresponding SaveFileFormat enum value
    static SaveFileFormat GetSaveFormatFromExtension(string ext)
    {
        return ext switch
        {
            ".vsdx" => SaveFileFormat.Vsdx,
            ".vsd"  => SaveFileFormat.Vsd,
            ".vdx"  => SaveFileFormat.Vdx,
            ".vsx"  => SaveFileFormat.Vsx,
            ".vtx"  => SaveFileFormat.Vtx,
            ".vssx" => SaveFileFormat.Vssx,
            ".vstx" => SaveFileFormat.Vstx,
            ".vsdm" => SaveFileFormat.Vsdm,
            ".vssm" => SaveFileFormat.Vssm,
            ".vstm" => SaveFileFormat.Vstm,
            _ => throw new NotSupportedException($"Extension '{ext}' is not supported.")
        };
    }
}