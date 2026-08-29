using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Get the first (default) page
        Page page = diagram.Pages[0];

        // Draw a rectangle shape on the page
        // Parameters: pinX, pinY, width, height (all in inches)
        long shapeId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);

        // Retrieve the Shape object using the returned ID
        Shape shape = page.Shapes.GetShape(shapeId);

        // Create a new Hyperlink and set its properties
        Hyperlink link = new Hyperlink();
        link.Name = "MyLink";
        link.Address.Value = "https://example.com";
        link.Description.Value = "Example website";

        // Add the hyperlink to the shape's Hyperlinks collection
        shape.Hyperlinks.Add(link);

        // Create a SolutionXML element that records the hyperlink information
        SolutionXML solXml = new SolutionXML();
        solXml.Name = "HyperlinkInfo";
        solXml.XmlValue = $"<Hyperlink Name=\"{link.Name}\" URL=\"{link.Address.Value}\" Description=\"{link.Description.Value}\"/>";

        // Add the SolutionXML element to the diagram
        diagram.SolutionXMLs.Add(solXml);

        // Save the diagram to a VSDX file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
