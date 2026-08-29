using System.IO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // List to hold extracted shape IDs and names
            List<(long Id, string Name)> shapeInfo = new List<(long, string)>();

            // Iterate through each SolutionXML attached to the diagram
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                // The XML content stored in the SolutionXML object
                string xmlContent = solXml.XmlValue;
                if (string.IsNullOrWhiteSpace(xmlContent))
                    continue;

                // Parse the XML using LINQ to XML
                XDocument xDoc = XDocument.Parse(xmlContent);

                // Assuming shape information is stored in elements named "Shape"
                // with attributes "ID" and "Name". Adjust element/attribute names as needed.
                foreach (XElement shapeElem in xDoc.Descendants("Shape"))
                {
                    XAttribute idAttr = shapeElem.Attribute("ID");
                    XAttribute nameAttr = shapeElem.Attribute("Name");

                    if (idAttr != null && nameAttr != null &&
                        long.TryParse(idAttr.Value, out long shapeId))
                    {
                        shapeInfo.Add((shapeId, nameAttr.Value));
                    }
                }
            }

            // Output the extracted IDs and Names
            foreach (var (Id, Name) in shapeInfo)
            {
                Console.WriteLine($"Shape ID: {Id}, Name: {Name}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
