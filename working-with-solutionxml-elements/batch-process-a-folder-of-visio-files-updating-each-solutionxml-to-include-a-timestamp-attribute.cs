using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;

public class Program
{
    // Entry point of the console application
    public static void Main(string[] args)
    {
        // Determine the folder to process
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the full path of the folder containing Visio files: ");
            folderPath = Console.ReadLine()?.Trim() ?? string.Empty;
        }

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine("Folder does not exist: " + folderPath);
            return;
        }

        // Process each Visio file in the folder (non‑recursive)
        string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            // Consider only supported Visio extensions
            if (!IsSupportedVisioExtension(ext))
                continue;

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Update each SolutionXML element
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    if (string.IsNullOrWhiteSpace(solXml.XmlValue))
                        continue;

                    // Parse the existing XML
                    XDocument xDoc = XDocument.Parse(solXml.XmlValue);

                    // Add or update the timestamp attribute on the root element
                    XElement root = xDoc.Root;
                    if (root != null)
                    {
                        root.SetAttributeValue("timestamp", DateTime.UtcNow.ToString("o"));
                        // Write back the modified XML
                        solXml.XmlValue = xDoc.Declaration != null
                            ? xDoc.Declaration + Environment.NewLine + xDoc.ToString()
                            : xDoc.ToString();
                    }
                }

                // Save the diagram back using the original format
                SaveFileFormat format = GetSaveFormat(ext);
                diagram.Save(filePath, format);
                Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }
    }

    // Determines if the file extension corresponds to a supported Visio format
    private static bool IsSupportedVisioExtension(string extension)
    {
        return extension switch
        {
            ".vsdx" => true,
            ".vsd" => true,
            ".vdx" => true,
            ".vsx" => true,
            ".vtx" => true,
            ".vssx" => true,
            ".vss" => true,
            ".vstx" => true,
            ".vst" => true,
            ".vsdm" => true,
            ".vssm" => true,
            ".vstm" => true,
            _ => false,
        };
    }

    // Maps a file extension to the corresponding SaveFileFormat enum value
    private static SaveFileFormat GetSaveFormat(string extension)
    {
        return extension switch
        {
            ".vsdx" => SaveFileFormat.Vsdx,
            ".vsd" => SaveFileFormat.Vsd,
            ".vdx" => SaveFileFormat.Vdx,
            ".vsx" => SaveFileFormat.Vsx,
            ".vtx" => SaveFileFormat.Vtx,
            ".vssx" => SaveFileFormat.Vssx,
            ".vss" => SaveFileFormat.Vss,
            ".vstx" => SaveFileFormat.Vstx,
            ".vst" => SaveFileFormat.Vst,
            ".vsdm" => SaveFileFormat.Vsdm,
            ".vssm" => SaveFileFormat.Vssm,
            ".vstm" => SaveFileFormat.Vstm,
            _ => SaveFileFormat.Vsdx, // Default fallback
        };
    }
}
