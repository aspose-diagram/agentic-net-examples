using System.IO;
using System;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths to the Visio file and the XML mapping file
            string diagramPath = "input.vsdx";
            string xmlPath = "mapping.xml";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Load the XML mapping document
                XDocument xdoc = XDocument.Load(xmlPath);

                // Build a dictionary: shape ID -> data values
                var mappings = xdoc.Root?
                    .Elements("Shape")
                    .Select(e => new
                    {
                        Id = (long?) (int?) e.Attribute("id") ?? 0,
                        Data1 = (string) e.Attribute("data1"),
                        Data2 = (string) e.Attribute("data2")
                    })
                    .Where(m => m.Id != 0)
                    .ToDictionary(m => m.Id);

                if (mappings != null && mappings.Count > 0)
                {
                    // Iterate through all pages and shapes, applying the mapping
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (mappings.TryGetValue(shape.ID, out var map))
                            {
                                if (map.Data1 != null)
                                    shape.Data1 = map.Data1;   // Shape data properties are simple strings
                                if (map.Data2 != null)
                                    shape.Data2 = map.Data2;
                            }
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
