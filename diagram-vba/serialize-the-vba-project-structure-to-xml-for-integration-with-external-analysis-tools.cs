using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputXmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the VBA project
            VbaProject vbaProject = diagram.VbaProject;
            if (vbaProject == null)
            {
                Console.Error.WriteLine("The diagram does not contain a VBA project.");
                return;
            }

            // Build XML representation of the VBA project
            XElement root = new XElement("VbaProject",
                new XAttribute("Name", vbaProject.Name ?? string.Empty),
                new XAttribute("IsSigned", vbaProject.IsSigned));

            XElement modulesElement = new XElement("Modules");

            // Iterate through all VBA modules
            foreach (VbaModule module in vbaProject.Modules)
            {
                XElement moduleElement = new XElement("Module",
                    new XAttribute("Name", module.Name ?? string.Empty),
                    new XElement("Code", new XCData(module.Codes ?? string.Empty))
                );
                modulesElement.Add(moduleElement);
            }

            root.Add(modulesElement);
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);

            // Ensure the output directory exists
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Save the XML to the specified file
            doc.Save(outputPath);
            Console.WriteLine($"VBA project serialized to XML successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}