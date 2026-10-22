using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram file path, masters XML file path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <mastersXmlPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string xmlPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Masters XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Load and parse the XML containing master definitions
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Expect XML structure: <Masters><Master>...</Master></Masters>
            XmlNodeList masterNodes = xmlDoc.SelectNodes("//Masters/Master");
            if (masterNodes == null || masterNodes.Count == 0)
            {
                Console.Error.WriteLine("No master definitions found in the XML file.");
                return;
            }

            // Iterate over each master definition and import it into the diagram
            foreach (XmlNode masterNode in masterNodes)
            {
                // Extract required information
                string stencilPath = masterNode.SelectSingleNode("StencilPath")?.InnerText?.Trim();
                string masterName = masterNode.SelectSingleNode("MasterName")?.InnerText?.Trim();

                if (string.IsNullOrEmpty(stencilPath) || string.IsNullOrEmpty(masterName))
                {
                    Console.Error.WriteLine("Master definition missing StencilPath or MasterName. Skipping this entry.");
                    continue;
                }

                // Guard against missing stencil file
                if (!File.Exists(stencilPath))
                {
                    Console.Error.WriteLine($"Stencil file not found: {stencilPath}. Skipping master '{masterName}'.");
                    continue;
                }

                // Import the master from the stencil into the diagram
                // Diagram.AddMaster returns the ID of the added master (long), but we don't need it here
                diagram.AddMaster(stencilPath, masterName);
                Console.WriteLine($"Imported master '{masterName}' from stencil '{stencilPath}'.");
            }

            // Save the updated diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}