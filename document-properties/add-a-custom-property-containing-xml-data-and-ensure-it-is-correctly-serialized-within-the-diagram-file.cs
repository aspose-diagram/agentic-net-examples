using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new Visio diagram
        Diagram diagram = new Diagram();

        // Define XML data to store as a custom property
        string xmlContent = "<mydata><value>123</value></mydata>";

        // Create a SolutionXML object with a unique name and the XML content
        SolutionXML customXml = new SolutionXML("MyCustomData", xmlContent);

        // Add the custom XML to the diagram's SolutionXML collection
        diagram.SolutionXMLs.Add(customXml);

        // Save the diagram to a VDX file (Visio XML format)
        diagram.Save("CustomPropertyDiagram.vdx", SaveFileFormat.Vdx);
    }
}
