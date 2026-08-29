using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Paths to the original VSDX file and the output file.
        string inputPath = "original.vsdx";
        string outputPath = "modified.vsdx";

        // Load the diagram from the VSDX package.
        Diagram diagram = new Diagram(inputPath);

        // Define the name of the SolutionXML entry and the new XML content.
        string solutionXmlName = "MyCustomData";
        string newXmlContent = @"<root xmlns=""http://example.com""><value>123</value></root>";

        // Search for an existing SolutionXML with the same name.
        SolutionXML targetXml = null;
        foreach (SolutionXML sx in diagram.SolutionXMLs)
        {
            if (sx.Name == solutionXmlName)
            {
                targetXml = sx;
                break;
            }
        }

        // Update the existing entry or add a new one.
        if (targetXml != null)
        {
            targetXml.XmlValue = newXmlContent;
        }
        else
        {
            diagram.SolutionXMLs.Add(new SolutionXML(solutionXmlName, newXmlContent));
        }

        // Save the diagram back to a VSDX package, preserving all resources.
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
