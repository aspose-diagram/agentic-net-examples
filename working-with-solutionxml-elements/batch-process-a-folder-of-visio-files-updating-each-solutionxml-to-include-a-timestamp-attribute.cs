using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <folderPath>");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Supported Visio extensions.
        string[] extensions = new[] { ".vsdx", ".vsd", ".vsx", ".vtx", ".vdx" };

        // Get all files with supported extensions.
        string[] files = Directory.GetFiles(folderPath);
        foreach (string filePath in files)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(extensions, ext) < 0)
                continue; // Skip non‑Visio files.

            // Guard: ensure the file exists (should always be true here).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Iterate over all SolutionXML elements.
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    if (solXml == null || string.IsNullOrEmpty(solXml.XmlValue))
                        continue;

                    try
                    {
                        // Parse the existing XML.
                        XDocument xDoc = XDocument.Parse(solXml.XmlValue);

                        // Add or update the timestamp attribute on the root element.
                        XElement root = xDoc.Root;
                        if (root != null)
                        {
                            root.SetAttributeValue("timestamp", DateTime.UtcNow.ToString("o"));
                            // Write the modified XML back to the SolutionXML.
                            solXml.XmlValue = xDoc.Declaration != null
                                ? xDoc.Declaration + Environment.NewLine + xDoc.ToString()
                                : xDoc.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        // If XML parsing fails, report and continue with next SolutionXML.
                        Console.Error.WriteLine($"Failed to process SolutionXML in file '{filePath}': {ex.Message}");
                    }
                }

                // Determine the appropriate save format based on original extension.
                SaveFileFormat format = GetSaveFileFormat(ext);

                // Save the diagram, overwriting the original file.
                diagram.Save(filePath, format);
                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur while loading or saving the diagram.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Maps a file extension to the corresponding Aspose.Diagram SaveFileFormat.
    /// </summary>
    private static SaveFileFormat GetSaveFileFormat(string extension)
    {
        switch (extension)
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vsx":
                return SaveFileFormat.Vsx;
            case ".vtx":
                return SaveFileFormat.Vtx;
            case ".vdx":
                return SaveFileFormat.Vdx;
            default:
                // Default to Vsdx if unknown; callers should have filtered extensions already.
                return SaveFileFormat.Vsdx;
        }
    }
}