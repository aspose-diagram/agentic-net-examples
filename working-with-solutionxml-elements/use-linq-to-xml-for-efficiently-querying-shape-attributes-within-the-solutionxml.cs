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
            var diagram = new Diagram("input.vsdx");

            // Retrieve a specific SolutionXML by its name (adjust the name as needed)
            var solutionXml = diagram.SolutionXMLs["MySolutionXML"];
            if (solutionXml == null)
            {
                Console.WriteLine("SolutionXML with the specified name was not found.");
                return;
            }

            // Parse the XML content stored in the SolutionXML
            XDocument xmlDoc = XDocument.Parse(solutionXml.XmlValue);

            // Example: Query all <Shape> elements and extract selected attributes
            var shapeAttributes = xmlDoc
                .Descendants()
                .Where(e => e.Name.LocalName.Equals("Shape", StringComparison.OrdinalIgnoreCase))
                .Select(e => new
                {
                    Id = (string)e.Attribute("ID"),
                    Name = (string)e.Attribute("Name"),
                    Type = (string)e.Attribute("Type")
                })
                .ToList();

            // Output the queried attributes
            foreach (var shape in shapeAttributes)
            {
                Console.WriteLine($"Shape ID: {shape.Id}, Name: {shape.Name}, Type: {shape.Type}");
            }

            // Optionally, save changes back to the diagram (if any modifications were made)
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
