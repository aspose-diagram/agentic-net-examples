using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        Diagram diagram = new Diagram();

        // XML defining a conditional formatting rule that highlights shapes
        // where the Data1 cell value is greater than 100 with a red fill.
        string conditionalXml = @"<ConditionalFormatting>
  <Rule>
    <Condition>Data1 > 100</Condition>
    <HighlightColor>#FF0000</HighlightColor>
  </Rule>
</ConditionalFormatting>";

        // Create a SolutionXML element and assign the name and XML content
        SolutionXML solXml = new SolutionXML();
        solXml.Name = "ConditionalFormatting";
        solXml.XmlValue = conditionalXml;

        // Add the SolutionXML element to the diagram's collection
        diagram.SolutionXMLs.Add(solXml);

        // Save the diagram to a VSDX file
        diagram.Save("ConditionalFormattingDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
