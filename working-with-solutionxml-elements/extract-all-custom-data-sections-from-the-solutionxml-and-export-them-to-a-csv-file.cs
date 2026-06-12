using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Prepare the CSV output file
            using (StreamWriter writer = new StreamWriter("output.csv", false, Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("SolutionXMLName,CustomDataSection");

                // Iterate over all SolutionXML entries in the diagram
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    // Skip if the XML value is empty
                    if (string.IsNullOrEmpty(solXml.XmlValue))
                        continue;

                    // Parse the XML content; if parsing fails, skip this entry
                    XDocument xDoc;
                    try
                    {
                        xDoc = XDocument.Parse(solXml.XmlValue);
                    }
                    catch
                    {
                        continue;
                    }

                    // Find all elements named "CustomData" regardless of namespace
                    var customDataElements = xDoc.Descendants()
                                                 .Where(e => e.Name.LocalName.Equals("CustomData", StringComparison.OrdinalIgnoreCase));

                    // Write each custom data section to the CSV
                    foreach (var elem in customDataElements)
                    {
                        // Escape double quotes for CSV compliance
                        string escapedXml = elem.ToString().Replace("\"", "\"\"");
                        writer.WriteLine($"\"{solXml.Name}\",\"{escapedXml}\"");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
