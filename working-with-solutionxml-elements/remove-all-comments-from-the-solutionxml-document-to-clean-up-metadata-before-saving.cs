using System;
using System.IO;
using System.Linq; // Required for OfType<T>()
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        string outputPath = "output.vsdx";

        Diagram diagram;
        try
        {
            // Load the Visio diagram
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Iterate through all SolutionXML elements and remove XML comments
        foreach (SolutionXML solXml in diagram.SolutionXMLs)
        {
            if (string.IsNullOrWhiteSpace(solXml.XmlValue))
                continue; // Skip empty entries

            XDocument xmlDoc;
            try
            {
                // Parse the XML content stored in the SolutionXML element
                xmlDoc = XDocument.Parse(solXml.XmlValue);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse SolutionXML '{solXml.Name}': {ex.Message}");
                continue; // Move to the next SolutionXML
            }

            // Remove all comment nodes from the parsed XML document
            foreach (var comment in xmlDoc.DescendantNodes().OfType<XComment>())
            {
                comment.Remove();
            }

            // Store the cleaned XML back into the SolutionXML element
            solXml.XmlValue = xmlDoc.ToString();
        }

        try
        {
            // Save the updated diagram with cleaned SolutionXML data
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("All SolutionXML comments have been removed and diagram saved.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}