using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Define a custom XML namespace to avoid collisions with other schemas
        string customNamespace = "http://example.com/custom";
        string xmlContent = $"<Root xmlns:custom=\"{customNamespace}\">" +
                            "<custom:Data>Sample value</custom:Data>" +
                            "</Root>";

        // Create a SolutionXML instance and assign the custom XML
        SolutionXML solutionXml = new SolutionXML();
        solutionXml.Name = "CustomNamespaceMapping";
        solutionXml.XmlValue = xmlContent;

        // Add the SolutionXML to the diagram's collection
        diagram.SolutionXMLs.Add(solutionXml);

        // Save the diagram to VSDX format
        string outputPath = "output.vsdx";
        diagram.Save(outputPath, SaveFileFormat.Vsdx);

        Console.WriteLine($"Diagram saved to '{outputPath}' with custom SolutionXML namespace mapping.");
    }
}
