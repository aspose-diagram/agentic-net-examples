using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputXmlFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : Path.ChangeExtension(inputPath, ".xml");

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Access the VBA project.
            VbaProject vbaProject = diagram.VbaProject;

            // Build XML representation of the VBA project.
            XElement root = new XElement("VbaProject",
                new XAttribute("Name", vbaProject.Name ?? string.Empty),
                new XAttribute("IsSigned", vbaProject.IsSigned ? BOOL.True.ToString() : BOOL.False.ToString()));

            XElement modulesElement = new XElement("Modules");

            // Iterate through all VBA modules.
            for (int i = 0; i < vbaProject.Modules.Count; i++)
            {
                VbaModule module = vbaProject.Modules[i];
                XElement moduleElement = new XElement("Module",
                    new XAttribute("Name", module.Name ?? string.Empty),
                    new XElement("Code", new XCData(module.Codes ?? string.Empty)));
                modulesElement.Add(moduleElement);
            }

            root.Add(modulesElement);
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);

            // Save the XML to the specified output path.
            doc.Save(outputPath);
            Console.WriteLine($"VBA project structure has been serialized to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}