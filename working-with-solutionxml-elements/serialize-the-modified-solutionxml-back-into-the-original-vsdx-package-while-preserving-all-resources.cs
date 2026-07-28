using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Paths to the original VSDX file and the output file
        string inputPath = "original.vsdx";
        string outputPath = "modified.vsdx";

        // Load the diagram from the VSDX package
        Diagram diagram = new Diagram(inputPath);

        // Define the name of the SolutionXML element and the new XML content
        string solutionXmlName = "CustomData";
        string newXmlContent = "<root><value>123</value></root>";

        // Try to locate an existing SolutionXML with the specified name
        SolutionXML existingXml = diagram.SolutionXMLs[solutionXmlName];

        if (existingXml != null)
        {
            // Update the XML value of the existing element
            existingXml.XmlValue = newXmlContent;
        }
        else
        {
            // Create a new SolutionXML instance and add it to the collection
            SolutionXML newSolutionXml = new SolutionXML(solutionXmlName, newXmlContent);
            diagram.SolutionXMLs.Add(newSolutionXml);
        }

        // Save the diagram back into a VSDX package, preserving all resources
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
