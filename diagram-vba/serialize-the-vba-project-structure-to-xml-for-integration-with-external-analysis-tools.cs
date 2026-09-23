using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <inputVisioFile> <outputXmlFile>");
            return;
        }

        string visioPath = args[0];
        string xmlOutputPath = args[1];

        // Load the Visio diagram
        Diagram diagram = new Diagram(visioPath);

        // Access the VBA project (read‑only)
        VbaProject vbaProject = diagram.VbaProject;

        // Create the root XML element for the VBA project
        XElement root = new XElement("VbaProject",
            new XAttribute("Name", vbaProject.Name ?? string.Empty),
            new XAttribute("IsSigned", vbaProject.IsSigned));

        // Iterate through all VBA modules and serialize their content
        foreach (VbaModule module in vbaProject.Modules)
        {
            XElement moduleElement = new XElement("Module",
                new XAttribute("Name", module.Name ?? string.Empty),
                new XElement("Code", new XCData(module.Codes ?? string.Empty))
            );

            root.Add(moduleElement);
        }

        // Build the XDocument and save to the specified XML file
        XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
        doc.Save(xmlOutputPath);

        Console.WriteLine($"VBA project serialized to XML at: {xmlOutputPath}");
    }
}
