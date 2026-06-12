using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Create a new diagram instance
        var diagram = new Aspose.Diagram.Diagram();

        // Define a unique name for the custom XML data set
        string solutionXmlName = "CustomData";

        // Prepare XML content with an explicit namespace to avoid collisions
        string customNamespace = "http://my.custom.namespace";
        string xmlContent = $@"
        <my:Root xmlns:my=""{customNamespace}"">
            <my:Item>SampleValue</my:Item>
        </my:Root>";

        // Create a SolutionXML object using the constructor that accepts name and XML value
        var solutionXml = new Aspose.Diagram.SolutionXML(solutionXmlName, xmlContent);

        // Add the SolutionXML object to the diagram's collection
        diagram.SolutionXMLs.Add(solutionXml);

        // (Optional) Save the diagram to a file – replace with your desired format and path
        diagram.Save("CustomNamespaceDiagram.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);
    }
}
