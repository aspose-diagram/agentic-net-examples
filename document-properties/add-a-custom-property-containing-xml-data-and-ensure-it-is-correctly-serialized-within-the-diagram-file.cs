using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new Visio diagram
        Diagram diagram = new Diagram();

        // XML data to store as a custom property
        string xmlData = "<root><item id=\"1\">Value</item></root>";

        // Create a SolutionXML object and assign name and XML value
        SolutionXML customXml = new SolutionXML
        {
            Name = "MyCustomXml",
            XmlValue = xmlData
        };

        // Add the custom XML to the diagram's SolutionXMLs collection
        diagram.SolutionXMLs.Add(customXml);

        // Save the diagram; the custom XML will be serialized within the file
        diagram.Save("CustomPropertyDiagram.vdx", SaveFileFormat.Vdx);
    }
}
