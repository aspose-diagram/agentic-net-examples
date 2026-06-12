using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the original and the target VSDX files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the SolutionXML name and its XML content
            string xmlName = "MyCustomData";
            string xmlContent = "<root><value>123</value></root>";

            // Check if a SolutionXML with the same name already exists
            SolutionXML existing = null;
            foreach (SolutionXML sx in diagram.SolutionXMLs)
            {
                if (sx.Name == xmlName)
                {
                    existing = sx;
                    break;
                }
            }

            if (existing != null)
            {
                // Update the existing entry
                existing.XmlValue = xmlContent;
            }
            else
            {
                // Add a new SolutionXML entry to the collection
                SolutionXML newXml = new SolutionXML(xmlName, xmlContent);
                diagram.SolutionXMLs.Add(newXml);
            }

            // Save the diagram back to a VSDX package, preserving all resources
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
