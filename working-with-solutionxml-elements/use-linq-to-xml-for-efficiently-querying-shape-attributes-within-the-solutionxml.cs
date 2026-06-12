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

            // Query all SolutionXML entries that have a specific name (e.g., "ShapeMetadata")
            var solutionXmlQuery = diagram.SolutionXMLs
                .Cast<SolutionXML>()                                   // Convert non‑generic collection to IEnumerable<SolutionXML>
                .Where(sxml => sxml.Name == "ShapeMetadata")           // Filter by the desired XML set name
                .Select(sxml => new
                {
                    Name = sxml.Name,
                    XmlDoc = XDocument.Parse(sxml.XmlValue)           // Parse the XML string into an XDocument for LINQ‑to‑XML queries
                });

            foreach (var entry in solutionXmlQuery)
            {
                // Example: find all <ShapeInfo> elements and read their "type" attribute
                var shapeInfos = entry.XmlDoc
                    .Descendants()
                    .Where(e => e.Name.LocalName == "ShapeInfo");

                foreach (var shapeInfo in shapeInfos)
                {
                    string shapeId = shapeInfo.Attribute("id")?.Value;
                    string shapeType = shapeInfo.Attribute("type")?.Value;

                    Console.WriteLine($"Shape ID: {shapeId}, Type: {shapeType}");
                }
            }

            // Save the diagram (if any modifications were made)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
