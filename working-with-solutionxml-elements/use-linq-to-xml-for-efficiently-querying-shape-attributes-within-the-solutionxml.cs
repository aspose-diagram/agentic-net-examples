using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Save the diagram to VDX (XML) format in a memory stream
            using (var ms = new MemoryStream())
            {
                diagram.Save(ms, SaveFileFormat.Vdx);
                ms.Position = 0; // Reset stream position for reading

                // Load the XML into XDocument for LINQ to XML queries
                var xDoc = XDocument.Load(ms);

                // Query all Shape elements and select desired attributes
                var shapeInfo = xDoc
                    .Descendants()
                    .Where(e => e.Name.LocalName == "Shape")
                    .Select(shape => new
                    {
                        Id = (string)shape.Attribute("ID"),
                        Name = (string)shape.Attribute("NameU"),
                        Type = (string)shape.Attribute("Type"),
                        // Example: retrieve a custom property named "Prop.MyCustomProp"
                        CustomProp = shape
                            .Descendants()
                            .FirstOrDefault(p => p.Name.LocalName == "Prop" && (string)p.Attribute("NameU") == "MyCustomProp")
                            ?.Attribute("Value")?.Value
                    })
                    .ToList();

                // Output the queried information
                foreach (var info in shapeInfo)
                {
                    Console.WriteLine($"Shape ID: {info.Id}, Name: {info.Name}, Type: {info.Type}, CustomProp: {info.CustomProp}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
