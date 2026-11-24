using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder to process
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the folder containing Visio files: ");
            folderPath = Console.ReadLine();
        }

        // Guard: folder must exist
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("Folder does not exist: " + folderPath);
            return;
        }

        // Define supported Visio extensions
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vss", ".vstx", ".vst" };

        // Get all files with supported extensions
        string[] files = Directory.GetFiles(folderPath);
        foreach (string filePath in files)
        {
            // Process only supported Visio files
            if (Array.IndexOf(extensions, Path.GetExtension(filePath).ToLower()) < 0)
                continue;

            // Guard: file must exist (redundant but per rule)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("File not found: " + filePath);
                continue;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate over all SolutionXML elements
                foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
                {
                    // Parse the existing XML value
                    XDocument xmlDoc;
                    try
                    {
                        xmlDoc = XDocument.Parse(solutionXml.XmlValue);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to parse SolutionXML '{solutionXml.Name}' in file '{filePath}': {ex.Message}");
                        continue;
                    }

                    // Add or update the timestamp attribute on the root element
                    XElement root = xmlDoc.Root;
                    if (root != null)
                    {
                        string timestamp = DateTime.UtcNow.ToString("o"); // ISO 8601 format
                        root.SetAttributeValue("timestamp", timestamp);
                        // Write the modified XML back to the SolutionXML object
                        solutionXml.XmlValue = xmlDoc.Declaration != null
                            ? xmlDoc.Declaration + Environment.NewLine + xmlDoc.ToString()
                            : xmlDoc.ToString();
                    }
                }

                // Determine appropriate save format based on original extension
                SaveFileFormat saveFormat = GetSaveFormat(Path.GetExtension(filePath));

                // Save the updated diagram, overwriting the original file
                diagram.Save(filePath, saveFormat);
                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                // Capture any Aspose.Diagram related errors
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Helper method to map file extensions to SaveFileFormat enum values
    private static SaveFileFormat GetSaveFormat(string extension)
    {
        switch (extension.ToLower())
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vdx":
                return SaveFileFormat.Vdx;
            case ".vssx":
                return SaveFileFormat.Vssx;
            case ".vss":
                return SaveFileFormat.Vss;
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vst":
                return SaveFileFormat.Vst;
            default:
                // Default to Vsdx if unknown
                return SaveFileFormat.Vsdx;
        }
    }
}