using System;
using System.IO;
using Aspose.Diagram;
using System.Xml.Linq;

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
            Console.Write("Enter the full path of the folder containing Visio files: ");
            folderPath = Console.ReadLine()?.Trim() ?? string.Empty;
        }

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine("Folder does not exist: " + folderPath);
            return;
        }

        // Process all Visio files in the folder (including subfolders if needed)
        string[] visioFiles = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);

        foreach (string filePath in visioFiles)
        {
            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Update each SolutionXML element with a timestamp attribute
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    if (string.IsNullOrWhiteSpace(solXml.XmlValue))
                        continue;

                    // Parse the existing XML
                    XDocument xDoc = XDocument.Parse(solXml.XmlValue);

                    // Ensure the root element exists
                    XElement root = xDoc.Root;
                    if (root != null)
                    {
                        // Add or update the timestamp attribute (ISO 8601 format)
                        root.SetAttributeValue("timestamp", DateTime.UtcNow.ToString("o"));
                        // Save the modified XML back to the SolutionXML object
                        solXml.XmlValue = xDoc.ToString();
                    }
                }

                // Save the modified diagram, overwriting the original file
                diagram.Save(filePath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processed and updated: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
