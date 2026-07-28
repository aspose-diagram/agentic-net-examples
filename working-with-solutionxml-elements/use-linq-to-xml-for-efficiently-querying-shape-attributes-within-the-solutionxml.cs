using System.IO;
using System;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access a specific SolutionXML by its name (replace with your actual name)
            SolutionXML solutionXml = diagram.SolutionXMLs["MyCustomData"];
            if (solutionXml == null)
            {
                Console.WriteLine("Specified SolutionXML not found.");
                return;
            }

            // Parse the XML stored in the SolutionXML into an XDocument for LINQ querying
            XDocument xmlDoc = XDocument.Parse(solutionXml.XmlValue);

            // Query all Shape elements and retrieve selected attributes (ID, Name, Data1)
            var shapeAttributes = xmlDoc.Descendants()
                                        .Where(e => e.Name.LocalName == "Shape")
                                        .Select(e => new
                                        {
                                            Id = (string)e.Attribute("ID"),
                                            Name = (string)e.Attribute("Name"),
                                            Data1 = (string)e.Attribute("Data1")
                                        })
                                        .ToList();

            // Output the queried attributes
            foreach (var shape in shapeAttributes)
            {
                Console.WriteLine($"ID: {shape.Id}, Name: {shape.Name}, Data1: {shape.Data1}");
            }

            // Example modification: update Data1 of the first Shape element
            XElement firstShape = xmlDoc.Descendants()
                                        .FirstOrDefault(e => e.Name.LocalName == "Shape");
            if (firstShape != null)
            {
                firstShape.SetAttributeValue("Data1", "UpdatedValue");
                // Write the modified XML back to the SolutionXML collection
                solutionXml.XmlValue = xmlDoc.ToString();
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
