using System.IO;
using System;
using Aspose.Diagram;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output XML file
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: ExportMasters <inputVisioFile> <outputXmlFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram
        using (Diagram diagram = new Diagram(inputPath))
        {
            // Create the root element for the XML document
            XElement root = new XElement("Masters");

            // Iterate over each master shape definition in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Build an XML element containing selected master metadata
                XElement masterElement = new XElement("Master",
                    new XElement("ID", master.ID),
                    new XElement("Name", master.Name),
                    new XElement("NameU", master.NameU),
                    new XElement("UniqueID", master.UniqueID.ToString()),
                    new XElement("Hidden", master.Hidden == BOOL.True ? "True" : "False"),
                    new XElement("ShapeCount", master.Shapes.Count)
                );

                root.Add(masterElement);
            }

            // Assemble the final XML document and save it to the specified path
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Master definitions exported to {outputPath}");
    }
}
