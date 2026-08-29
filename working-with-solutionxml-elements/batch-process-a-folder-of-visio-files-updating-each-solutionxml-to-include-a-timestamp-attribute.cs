using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class VisioBatchProcessor
{
    static void Main()
    {
        // Folder containing Visio files to process
        string folderPath = @"C:\VisioFiles";

        // Process each Visio file in the folder (adjust the search pattern as needed)
        foreach (string filePath in Directory.GetFiles(folderPath, "*.vsdx"))
        {
            // Load the Visio diagram from the file
            using (Diagram diagram = new Diagram(filePath))
            {
                // Prepare a timestamp XML fragment
                string timestamp = DateTime.UtcNow.ToString("o"); // ISO 8601 format
                string timestampXml = $"<Timestamp value=\"{timestamp}\" />";

                // Try to find an existing SolutionXML named "Timestamp"
                SolutionXML existing = diagram.SolutionXMLs
                                              .FirstOrDefault(s => s.Name == "Timestamp");

                if (existing != null)
                {
                    // Update the XML value of the existing entry
                    // Assuming the property to set the XML content is XmlValue
                    existing.XmlValue = timestampXml;
                }
                else
                {
                    // Add a new SolutionXML entry with the timestamp
                    diagram.SolutionXMLs.Add(new SolutionXML("Timestamp", timestampXml));
                }

                // Save the modified diagram back to the original file
                diagram.Save(filePath, SaveFileFormat.Vsdx);
            }
        }
    }
}
