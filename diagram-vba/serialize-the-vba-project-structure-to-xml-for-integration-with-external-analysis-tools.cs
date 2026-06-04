using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class VbaProjectSerializer
{
    // Serializes the VBA project structure of a Visio diagram to an XML file.
    public static void SerializeVbaProject(string diagramPath, string outputXmlPath)
    {
        // Load the Visio diagram (uses Aspose.Diagram's load rule).
        Diagram diagram = new Diagram(diagramPath);

        // Access the VBA project associated with the diagram.
        VbaProject vbaProject = diagram.VbaProject;

        // Build the root XML element with basic project attributes.
        XElement root = new XElement("VbaProject",
            new XAttribute("Name", vbaProject.Name ?? string.Empty),
            new XAttribute("IsSigned", vbaProject.IsSigned));

        // Serialize all VBA modules.
        XElement modulesElement = new XElement("Modules");
        foreach (VbaModule module in vbaProject.Modules)
        {
            modulesElement.Add(
                new XElement("Module",
                    new XAttribute("Name", module.Name ?? string.Empty),
                    new XAttribute("Type", module.Type.ToString()),
                    // Preserve the module code as CDATA to keep formatting.
                    new XCData(module.Codes ?? string.Empty)));
        }
        root.Add(modulesElement);

        // Serialize all VBA references.
        XElement referencesElement = new XElement("References");
        foreach (VbaProjectReference reference in vbaProject.References)
        {
            referencesElement.Add(
                new XElement("Reference",
                    new XAttribute("Name", reference.Name ?? string.Empty),
                    new XAttribute("Libid", reference.Libid ?? string.Empty),
                    new XAttribute("ExtendedLibid", reference.ExtendedLibid ?? string.Empty),
                    new XAttribute("RelativeLibid", reference.RelativeLibid ?? string.Empty),
                    new XAttribute("TwiddledLibid", reference.Twiddledlibid ?? string.Empty),
                    new XAttribute("Type", reference.Type.ToString())));
        }
        root.Add(referencesElement);

        // Create the XML document and save it (uses standard .NET save logic).
        XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
        doc.Save(outputXmlPath);
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file and output XML file paths.
            string diagramFile = "example.vsdx";
            string xmlOutput = "VbaProjectStructure.xml";

            // Perform the serialization.
            VbaProjectSerializer.SerializeVbaProject(diagramFile, xmlOutput);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}