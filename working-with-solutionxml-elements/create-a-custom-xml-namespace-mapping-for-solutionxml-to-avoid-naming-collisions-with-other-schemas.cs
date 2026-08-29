using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        Diagram diagram = new Diagram();

        // Define XML that uses a custom namespace to avoid collisions
        string customXml = @"<custom:Data xmlns:custom=""http://mycustomnamespace.com"">" +
                           @"<custom:Item>Value</custom:Item>" +
                           @"</custom:Data>";

        // Create a SolutionXML object with a unique name and the custom XML content
        SolutionXML solutionXml = new SolutionXML("MyCustomData", customXml);

        // Add the SolutionXML to the diagram's collection
        diagram.SolutionXMLs.Add(solutionXml);

        // Save the diagram to a file (choose any supported format)
        diagram.Save("CustomNamespaceDiagram.vdx", SaveFileFormat.Vdx);
    }
}
