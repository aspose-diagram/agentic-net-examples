using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new empty Visio diagram
            Diagram diagram = new Diagram();

            // Define the conditional formatting XML
            string formattingXml = @"
<ConditionalFormatting>
    <Rule>
        <Condition>Data1 > 100</Condition>
        <HighlightColor>#FF0000</HighlightColor>
    </Rule>
</ConditionalFormatting>";

            // Create a SolutionXML element and assign name and XML content
            SolutionXML solXml = new SolutionXML();
            solXml.Name = "ConditionalFormatting";
            solXml.XmlValue = formattingXml.Trim();

            // Add the SolutionXML to the diagram's collection
            diagram.SolutionXMLs.Add(solXml);

            // Save the diagram to a VSDX file
            diagram.Save("ConditionalFormatting.vsdx", SaveFileFormat.Vsdx);
        }
    }