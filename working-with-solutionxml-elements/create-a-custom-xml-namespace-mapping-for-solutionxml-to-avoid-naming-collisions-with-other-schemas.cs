using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new diagram instance
            Diagram diagram = new Diagram();

            // Define custom XML with a unique namespace to avoid collisions
            string customXml = @"<root xmlns:custom=""http://example.com/custom"">" +
                               @"<custom:Data>Sample Value</custom:Data>" +
                               @"</root>";

            // Create a SolutionXML object and assign name and XML content
            SolutionXML solutionXml = new SolutionXML();
            solutionXml.Name = "CustomSolutionData";
            solutionXml.XmlValue = customXml;

            // Add the SolutionXML to the diagram's collection
            diagram.SolutionXMLs.Add(solutionXml);

            // Save the diagram to a VSDX file
            diagram.Save("CustomSolutionDiagram.vsdx", SaveFileFormat.Vsdx);

            // Inform the user
            Console.WriteLine("Diagram saved with custom SolutionXML namespace mapping.");
        }
    }