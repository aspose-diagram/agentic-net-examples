using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputXmlFile>");
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

            // Create XML document root
            XDocument xmlDoc = new XDocument(new XElement("Masters"));

            // Iterate over all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Build XML element for each master
                XElement masterElement = new XElement("Master",
                    new XAttribute("ID", master.ID),
                    new XAttribute("Name", master.Name ?? string.Empty),
                    new XAttribute("NameU", master.NameU ?? string.Empty),
                    new XAttribute("UniqueID", master.UniqueID.ToString())
                );

                // Optionally include count of shapes defined in the master
                if (master.Shapes != null)
                {
                    masterElement.Add(new XAttribute("ShapeCount", master.Shapes.Count));
                }

                // Append master element to root
                xmlDoc.Root.Add(masterElement);
            }

            // Ensure output directory exists
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Save XML to file
            xmlDoc.Save(outputPath);
            Console.WriteLine($"Master definitions exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}