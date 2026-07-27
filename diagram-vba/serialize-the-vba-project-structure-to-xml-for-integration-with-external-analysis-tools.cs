using System.IO;
using System;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (uses Aspose.Diagram's load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the VBA project within the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Create the root XML element for the VBA project
            XElement root = new XElement("VbaProject",
                new XAttribute("Name", vbaProject.Name ?? string.Empty),
                new XAttribute("IsSigned", vbaProject.IsSigned));

            // Serialize all VBA modules
            XElement modulesElement = new XElement("Modules");
            foreach (VbaModule module in vbaProject.Modules)
            {
                modulesElement.Add(
                    new XElement("Module",
                        new XAttribute("Name", module.Name ?? string.Empty),
                        new XAttribute("Type", module.Type.ToString()),
                        new XElement("Codes", new XCData(module.Codes ?? string.Empty))
                    )
                );
            }
            root.Add(modulesElement);

            // Serialize all VBA references
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
                        new XAttribute("Type", reference.Type.ToString())
                    )
                );
            }
            root.Add(referencesElement);

            // Build the final XML document and save it
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            doc.Save("VbaProjectStructure.xml");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
