using System.IO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Paths to the Visio file, the XML data source, and the output file
            string diagramPath = "input.vsdx";
            string xmlPath = "data.xml";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Load the XML data source
            XDocument xmlDoc = XDocument.Load(xmlPath);
            IEnumerable<XElement> shapeElements = xmlDoc.Root?.Elements("Shape");

            if (shapeElements != null)
            {
                // Iterate through all pages and shapes in the diagram
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Find a matching XML element based on the shape's universal name (NameU)
                        XElement matchingElement = FindShapeElement(shapeElements, shape.NameU);
                        if (matchingElement != null)
                        {
                            // Update the shape's text
                            string newText = matchingElement.Element("Text")?.Value ?? string.Empty;
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt(newText));

                            // Optionally update the shape's fill color if provided in XML
                            XAttribute fillAttr = matchingElement.Attribute("FillColor");
                            if (fillAttr != null)
                            {
                                // Expecting a hex color string like "#FF0000"
                                shape.Fill.FillForegnd.Value = fillAttr.Value;
                            }
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to locate an XML element that matches a shape's NameU
    private static XElement FindShapeElement(IEnumerable<XElement> elements, string nameU)
    {
        foreach (XElement element in elements)
        {
            XAttribute nameAttr = element.Attribute("Name");
            if (nameAttr != null && nameAttr.Value == nameU)
            {
                return element;
            }
        }
        return null;
    }
}
